Imports Microsoft.Win32
Imports System.Threading

Public Class Form1
    Dim WaitThread As Thread = New Thread(AddressOf WaitM)
    Dim WaitMillisecond As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = False

        '监听用户改变系统时间
        AddHandler SystemEvents.TimeChanged, AddressOf UserChangeTime

        WaitThread.Start()
    End Sub

    Private Sub UserChangeTime(sender As Object, e As System.EventArgs)
        Me.Text = "外部修改了系统时间"
        If WaitThread.ThreadState = ThreadState.WaitSleepJoin Then WaitThread.Interrupt()
        If WaitThread.ThreadState = ThreadState.Running Then WaitThread.Abort()
        WaitThread = New Thread(AddressOf WaitM)

        WaitThread.Start()
    End Sub

    Private Sub WaitM()
        Do While True
            WaitMillisecond = 999 - Now.Millisecond + (59 - Now.Second) * 1000
            Me.Text = ("距离下次整分钟还有：" & WaitMillisecond & " 毫秒")
            Thread.Sleep(WaitMillisecond)
            Me.Text = ("整分钟咯！  " & Now.Minute)
        Loop
    End Sub

End Class