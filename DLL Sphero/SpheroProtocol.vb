Imports System.IO.Ports
Imports System.Threading
Public Class SpheroProtocol
    Public Event newData(ByVal data As Byte())

    Dim _port As SerialPort
    Dim inQueue As Queue(Of Byte)
    Dim outQueue As Queue(Of Byte())
    Dim _inTh As Thread
    Dim _outTh As Thread

    Sub New()
        Me.New("")
    End Sub
    Sub New(ByVal portName As String)
        _port = New SerialPort()
        inQueue = New Queue(Of Byte)()
        outQueue = New Queue(Of Byte())()

        Me.Port = portName
        _inTh = New Thread(AddressOf readInput)
        _outTh = New Thread(AddressOf writeOutput)

        _inTh.Start()
        _outTh.Start()
    End Sub

#Region "Connection"

    Private _portName As String
    Public Property Port() As String
        Get
            Return _portName
        End Get
        Set(ByVal value As String)
            _portName = value
        End Set
    End Property


    Public ReadOnly Property isConnected
        Get
            Return _port.IsOpen
        End Get
    End Property

    Public Sub connect()
        If Port <> String.Empty And Not isConnected Then
            _port.PortName = Port
            _port.Open()
        End If
    End Sub

    Public Sub openOnPort(ByVal portName As String)
        Port = portName
        connect()
    End Sub

    Public Sub disconnect()
        If isConnected Then
            _port.Close()
        End If
    End Sub

#End Region

    Dim seq As Short = 0
    Public Sub send(ByVal addr As Byte, ByVal command As Byte, ByVal data As Byte())
        send(addr, seq, command, data)
        seq += 1
        seq = seq Mod 255
    End Sub

    Public Sub send(ByVal addr As Byte, ByVal seq As Integer, ByVal command As Byte, ByVal data As Byte())

        Dim buff(6 + data.Count) As Byte

        buff(0) = 255
        buff(1) = 255
        buff(2) = addr
        buff(3) = command
        buff(4) = seq
        buff(5) = data.Length + 1 'cantidad de elementos hasta el final del paquete, es decir, todo data y crc
        For i As Integer = 0 To data.Count - 1
            buff(6 + i) = data(i)
        Next

        buff(buff.Count - 1) = getCRC(buff)

        outQueue.Enqueue(buff)


    End Sub

    Private Function getCRC(ByVal packet As Byte()) As Byte
        'asumo que lo que viene es el paquete completo, con el espacio de CRC incluido, sin importar su valor.
        Dim sum As Integer = 0
        For i As Integer = 2 To packet.Count - 2
            sum += packet(i)
        Next

        Dim crc As Byte = sum Mod 256
        crc = Not crc
        Return crc
    End Function

    Private Sub readInput()

        While True
            If isConnected Then
                For i As Integer = 0 To _port.BytesToRead
                    inQueue.Enqueue(_port.ReadByte)

                Next
                If (Not parsing) Then parsePacket()
                Thread.Sleep(10)
            End If

        End While
        Throw New Exception("Input reading stoped!")
    End Sub

    Private Sub writeOutput()
        While True
            If isConnected Then
                If outQueue.Count > 0 Then
                    Dim buff() As Byte = outQueue.Dequeue()
                    _port.Write(buff, 0, buff.Count)
                End If
            End If
            Thread.Sleep(10)
        End While
        Throw New Exception("output writing stoped!")
    End Sub

    Dim state As String = "h1"
    Dim parsing As Boolean = False
    Dim _currentPacket As List(Of Byte)
    Private Sub parsePacket()

        parsing = True

        While inQueue.Count > 0
            Dim b As Byte = inQueue.Dequeue

            Select Case state
                Case "h1"
                    If b = 255 Then
                        _currentPacket = New List(Of Byte)
                        _currentPacket.Add(b)
                        state = "h2"
                    End If
                Case "h2"
                    If b = 255 Or b = 254 Then
                        _currentPacket.Add(b)
                        state = "MRSP"
                    End If
                Case "MRSP"
                    'codigo de retorno.

                    '00h ORBOTIX_RSP_CODE_OK Command succeeded
                    '01h ORBOTIX_RSP_CODE_EGEN General, non-specific error
                    '02h ORBOTIX_RSP_CODE_ECHKSUM Received checksum failure
                    '03h ORBOTIX_RSP_CODE_EFRAG Received command fragment
                    '04h ORBOTIX_RSP_CODE_EBAD_CMD Unknown command ID
                    '05h ORBOTIX_RSP_CODE_EUNSUPP Command currently unsupported
                    '06h ORBOTIX_RSP_CODE_EBAD_MSG Bad message format
                    '07h ORBOTIX_RSP_CODE_EPARAM Parameter value(s) invalid
                    '08h ORBOTIX_RSP_CODE_EEXEC Failed to execute command
                    '09h ORBOTIX_RSP_CODE_EBAD_DID Unknown Device ID
                    '31h ORBOTIX_RSP_CODE_POWER_NOGOOD Voltage too low for reflash operation
                    '32h ORBOTIX_RSP_CODE_PAGE_ILLEGAL Illegal page number provided
                    '33h ORBOTIX_RSP_CODE_FLASH_FAIL Page did not reprogram correctly
                    '34h ORBOTIX_RSP_CODE_MA_CORRUPT Main Application corrupt
                    '35h ORBOTIX_RSP_CODE_MSG_TIMEOUT Msg state machine timed out

                    _currentPacket.Add(b)
                    state = "SEQ"
                Case "SEQ"
                    'echo del seq.
                    'en caso de ser async, es 0

                    _currentPacket.Add(b)
                    state = "DLEN"
                Case "DLEN"

                    _currentPacket.Add(b)
                    If (b = 1) Then
                        state = "CRC"
                    Else
                        state = "DATA"
                    End If


                Case "DATA"
                    'el 5to byte es el length
                    _currentPacket.Add(b)
                    If (_currentPacket.Count - 5 = _currentPacket(4) - 1) Then
                        state = "CRC"
                    End If
                Case "CRC"
                    _currentPacket.Add(b)

                    If (_currentPacket.Last = getCRC(_currentPacket.ToArray())) Then
                        RaiseEvent newData(_currentPacket.ToArray)
                    Else
                        Console.WriteLine("crc error")
                    End If
                    state = "h1"
                Case Else

            End Select



        End While

        parsing = False

    End Sub

End Class
