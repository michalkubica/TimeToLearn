using System;
using System.Collections.ObjectModel;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

//todo
//notifications and reacting to reply
//app icon

namespace LearningTimer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    enum AppState { Idle, Learn, Break };
    public sealed partial class MainPage : Page
    {
        ObservableCollection<int> hours = new ObservableCollection<int>();
        ObservableCollection<int> minutes = new ObservableCollection<int>();
        ObservableCollection<int> seconds = new ObservableCollection<int>();
        ObservableCollection<int> repeat = new ObservableCollection<int>();
        DispatcherTimer timer;
        ProgramTimer timerSets;
        int secondsElapsed = 0;
        AppState currentState = AppState.Idle;

        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(200,500);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            for(short i = 0; i < 4; i++) hours.Add(i);
            for(short i = 0; i < 60; i++) minutes.Add(i);
            for(short i = 0; i < 60; i++) seconds.Add(i);
            for(short i = 1; i <= 8; i++) repeat.Add(i);
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            createTimer();
            timer.Start();
            secondsElapsed++;
            buttonStart.IsEnabled = false;
            buttonStop.IsEnabled = true;
            disableSettings();
            timerSets = new ProgramTimer(boxLearningH, boxLearningM, boxLearningS, boxBreakH, boxBreakM, boxBreakS, boxRepeatTimes);
            updateAppState(AppState.Learn);
            updateCycle();
        }

        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            resetProcedure();
            updateCycle(0);
        }

        public void dispatcherTimer_Tick(object sender, object e)
        {
            if (timerSets.checkFinish(secondsElapsed, currentState))
            {
                if (timerSets.lastCycle() && currentState == AppState.Break)
                {
                    timer.Stop();
                    resetProcedure();
                }
                else
                {
                    timer.Stop();
                    secondsElapsed = 0;
                    updateDisplayedTime();
                    createTimer();
                    timer.Start();
                    secondsElapsed++;
                    if (currentState == AppState.Learn) updateAppState(AppState.Break);
                    else { updateAppState(AppState.Learn); updateCycle(); }
                }
            }
            else
            {
                updateDisplayedTime();
                secondsElapsed++;
            } 
        }

        void updateDisplayedTime()
        {
            string timePresented = "";
            if ((secondsElapsed / 3600) < 10) timePresented += "0" + (secondsElapsed / 3600).ToString() + ":";
            else timePresented += (secondsElapsed / 3600).ToString() + ":";
            if ((secondsElapsed / 60) < 10) timePresented += "0" + (secondsElapsed / 60).ToString() + ":";
            else timePresented += (secondsElapsed / 60).ToString() + ":";
            if ((secondsElapsed % 60) < 10) timePresented += "0" + (secondsElapsed % 60).ToString();
            else timePresented += (secondsElapsed % 60).ToString();
            textElapsedTime.Text = timePresented;
        }

        void disableSettings()
        {
            boxLearningH.IsEnabled = false;
            boxLearningM.IsEnabled = false;
            boxLearningS.IsEnabled = false;
            boxBreakH.IsEnabled = false;
            boxBreakM.IsEnabled = false;
            boxBreakS.IsEnabled = false;
            boxRepeatTimes.IsEnabled = false;
        }

        void enableSettings()
        {
            boxLearningH.IsEnabled = true;
            boxLearningM.IsEnabled = true;
            boxLearningS.IsEnabled = true;
            boxBreakH.IsEnabled = true;
            boxBreakM.IsEnabled = true;
            boxBreakS.IsEnabled = true;
            boxRepeatTimes.IsEnabled = true;
        }

        void updateAppState(AppState a)
        {
            currentState = a;
            textState.Text = a.ToString(); 
        }

        void updateCycle()
        {
            timerSets.nextCycle();
            textCycleNo.Text = timerSets.getCurrentCycle().ToString();
        }

        void updateCycle(int c)
        {
            timerSets.setCycle(c);
            textCycleNo.Text = timerSets.getCurrentCycle().ToString();
        }

        void createTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += dispatcherTimer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
        }

        void resetProcedure()
        {
            secondsElapsed = 0;
            updateDisplayedTime();
            buttonStop.IsEnabled = false;
            buttonStart.IsEnabled = true;
            enableSettings();
            updateAppState(AppState.Idle);
            updateCycle();
        }
    }
}

