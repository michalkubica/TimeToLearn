using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace LearningTimer
{
    class ProgramTimer
    {
        private int totalSecondsLearn;
        private int totalSecondsBreak;
        private int totalCycles;
        private int currentCycle;

        public ProgramTimer(ComboBox lh, ComboBox lm, ComboBox ls, ComboBox bh, ComboBox bm, ComboBox bs, ComboBox tc)
        {
            totalSecondsLearn = 3600 * int.Parse(lh.SelectedItem.ToString()) + 60 * int.Parse(lm.SelectedItem.ToString()) + int.Parse(ls.SelectedItem.ToString());
            totalSecondsBreak = 3600 * int.Parse(bh.SelectedItem.ToString()) + 60 * int.Parse(bm.SelectedItem.ToString()) + int.Parse(bs.SelectedItem.ToString());
            totalCycles = int.Parse(tc.SelectedItem.ToString());
            currentCycle = 0;
        }

        public bool checkFinish(int elapsed, AppState s)
        {
            if (s == AppState.Learn) return elapsed == totalSecondsLearn;
            else if (s == AppState.Break) return elapsed == totalSecondsBreak;
            else return true;
        }

        public void nextCycle()
        {
            if (currentCycle == totalCycles) currentCycle = 0;
            else currentCycle++;
        }

        public void setCycle(int i)
        {
            currentCycle = i;
        }

        public int getCurrentCycle()
        {
            return currentCycle;
        }

        public bool lastCycle()
        {
            return currentCycle == totalCycles;
        }
    }
}
