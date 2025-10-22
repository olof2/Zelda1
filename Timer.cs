using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zelda1
{
    internal class Timer
    {
        double currentTime = 0.0;

        public void resetAndStart(double delay) { currentTime = delay; }

        public bool isDone() { return currentTime <= 0.0; }

        public void Update(double deltaTime) { currentTime -= deltaTime; }
    }
}
