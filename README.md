<h1>Move Your ball and play sound</h1>

Proof of concept made for KISS 2013 (Kyma International Sound Symposium - http://http://kiss2013.symbolicsound.com ).

<h2>How to communicate Sphero and kyma ?</h2>
There are two communication protocols:<br />
First is the Bluetooth V3.0 protocol allowing the sphero to be connected to pc.<br />
The second is the OSC message which will be used to control the kyma via ethernet connection.<br />

The goal is to control kyma with the Sphero ball. This application is strictly limited, but the source code allows an evolution according to your imagination.

<h2>How does it work?</h2>
I use an OSC C# class made by Mingming Zhang to send OSC message, I've just add one function to disable the OSC connection.
I use also a SDK Sphero to communicate with the pc.

<h3>How to create sound ?</h3>
First, you connect the ball to the pc with a bluetooth adapter. After that you lunch \Sphero-Music\WindowsFormsApplication1\bin\Debug\Sphero-music.exe
 
<h3>Connect the ball to the pc and Kyma</h3>
When the program is running you click on Connect.
To send OSC Message to Kyma you put the IP Address and Port and click on OK.

<h3>Create Sound</h3>
Move the ball the way you want and listen to what you create.