Imports System.Drawing
Public Class Sphero
    Sub New(ByVal PortName As String)
        _protocol = New SpheroProtocol(PortName)
        AddHandler _protocol.newData, AddressOf updateData
    End Sub
    Dim _protocol As SpheroProtocol
#Region "connection"
    Public Sub connect()

        _protocol.connect()
        If isConnected() Then initializeSampling()
    End Sub
    Public Sub disconnect()
        _protocol.disconnect()
    End Sub
    Public Function isConnected() As Boolean
        Return _protocol.isConnected
    End Function
#End Region

#Region "properties"
#Region "Read Only"
    Private _yaw As Integer
    Public ReadOnly Property Yaw() As Integer
        Get
            Return _yaw
        End Get
    End Property

    Private _pitch As Integer
    Public ReadOnly Property Pitch() As Integer
        Get
            Return _pitch
        End Get
    End Property

    Private _roll As Integer
    Public ReadOnly Property Roll() As Integer
        Get
            Return _roll
        End Get
    End Property

    Private _accelX As Integer
    Public ReadOnly Property AccelX() As Integer
        Get
            Return _accelX
        End Get
    End Property

    Private _accelY As Integer
    Public ReadOnly Property AccelY() As Integer
        Get
            Return _accelY
        End Get
    End Property

    Private _accelZ As Integer
    Public ReadOnly Property AccelZ() As Integer
        Get
            Return _accelZ
        End Get
    End Property

    Private _gyroX As Integer
    Public ReadOnly Property GyroX() As Integer
        Get
            Return _gyroX
        End Get
    End Property

    Private _gyroY As Integer
    Public ReadOnly Property GyroY() As Integer
        Get
            Return _gyroY
        End Get
    End Property

    Private _gyroZ As Integer
    Public ReadOnly Property GyroZ() As Integer
        Get
            Return _gyroZ
        End Get
    End Property

    Private _battery As Integer
    Public ReadOnly Property BatteryVoltage() As Integer
        Get
            Return _battery
        End Get
    End Property

    Private _powerState As PowerState
    Public ReadOnly Property PowerState() As PowerState
        Get
            Return _powerState
        End Get
    End Property

    Private _numCharges As Integer
    Public ReadOnly Property NumCharges() As Integer
        Get
            Return _numCharges
        End Get
    End Property

    Private _timeSinceCharge As Integer
    Public ReadOnly Property TimeSinceCharge() As Integer
        Get
            Return _timeSinceCharge
        End Get
    End Property
#End Region

    Private _color As Color
    Public Property Color() As Color
        Get
            Return _color
        End Get
        Set(ByVal value As Color)
            _color = value
            Dim b(3) As Byte
            b(0) = value.R
            b(1) = value.G
            b(2) = value.B
            b(3) = 0
            _protocol.send(2, &H20, b)
        End Set
    End Property

    Private _heading As Integer
    Public Property Heading() As Integer
        Get
            Return _heading
        End Get
        Set(ByVal value As Integer)
            _heading = value Mod 360
            If _heading < 0 Then _heading = 360 + _heading
            sendRollCommand()
        End Set
    End Property

    Private _speed As Byte
    Public Property Speed() As Byte
        Get
            Return _speed
        End Get
        Set(ByVal value As Byte)
            _speed = value Mod 256
            sendRollCommand()
        End Set
    End Property

    Private _stabilization As Boolean
    Public Property Stabilization() As Boolean
        Get
            Return _stabilization
        End Get
        Set(ByVal value As Boolean)
            _stabilization = value
            Dim b(0) As Byte
            b(0) = CByte(value)
            _protocol.send(2, &H2, b)
        End Set
    End Property

    Private _backLedOutput As Byte
    Public Property BackLedOutput() As Byte
        Get
            Return _backLedOutput
        End Get
        Set(ByVal value As Byte)
            _backLedOutput = value
            Dim b(0) As Byte
            b(0) = value
            _protocol.send(2, &H21, b)
        End Set
    End Property
#End Region

    Private Sub initializeSampling()
        'this asks for the data of the yaw, roll and pitch
        'Dim b(12) As Byte
        Dim b(8) As Byte
        b(0) = 0 'n1
        b(1) = &HA 'n2
        b(2) = 0 'm1
        b(3) = 1 ' m2
        b(4) = 0 'mask1
        b(5) = 7 'mask1 2
        b(6) = &HFC 'mask1 3
        b(7) = 0 'mask1 4
        b(8) = 0 'packet limit
        'b(9) = 0 'mask2
        'b(10) = 0 'mask2 2
        'b(11) = 1 'mask2 3
        'b(12) = &H80 'mask2 4
        _protocol.send(2, &H11, b)

    End Sub

    Private Sub sendRollCommand()
        Dim b(3) As Byte
        b(0) = Speed
        b(1) = (Heading >> 8)
        b(2) = (Heading And 255)
        b(3) = 1
        _protocol.send(2, &H30, b)
    End Sub

    Private Function Bitsigned16(ByVal a As Integer) As Integer
        If a > (2 ^ 16) / 2 Then
            Return -1 * ((2 ^ 16) - a)
        Else
            Return a
        End If

    End Function

    Private Function get16bitData(ByVal index As Integer, ByVal source As Byte()) As Integer
        Dim temp As Integer
        temp = source(index)
        temp <<= 8
        temp = temp Or source(index + 1)
        Return temp
    End Function

    Dim _lastUpdate As DateTime
    Dim _updateInterval As New TimeSpan(0, 0, 1)

    Private Sub updateData(ByVal Data As Byte())
        Dim _now As DateTime = Now()
        If (Now - _lastUpdate > _updateInterval) Then
            _lastUpdate = _now
            sendGetPowerStateCommand()
        End If
        If Data(2) = 3 Then
            'Sensor(Data)
            _pitch = Bitsigned16(get16bitData(5, Data))
            _roll = Bitsigned16(get16bitData(7, Data))
            _yaw = Bitsigned16(get16bitData(9, Data))
            _accelX = Bitsigned16(get16bitData(11, Data))
            _accelY = Bitsigned16(get16bitData(13, Data))
            _accelZ = Bitsigned16(get16bitData(15, Data))
            _gyroX = Bitsigned16(get16bitData(17, Data))
            _gyroY = Bitsigned16(get16bitData(19, Data))
            _gyroZ = Bitsigned16(get16bitData(21, Data))

        End If

        If Data(3) = 255 Then
            'echo of the sequence number sent. We only sent 255 as seq if the packet is a GetPowerStateCommand
            If Data(5) = 1 Then
                _powerState = Data(6)
                _battery = get16bitData(7, Data)
                _numCharges = get16bitData(9, Data)
                _timeSinceCharge = get16bitData(11, Data)
            End If
        End If
    End Sub

    Private Sub sendGetPowerStateCommand()
        _protocol.send(0, 255, &H20, {})
    End Sub
 




End Class

Public Enum PowerState
    Invalid = 0
    BatteryCharging
    BatteryOk
    BatteryLow
    BatteryCritical
End Enum