using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PatternQueue
    {
        private readonly Queue<SquareScript> _queue;
        public Color EmphasizeColor;

        public PatternQueue(int size)
        {
            _queue = new Queue<SquareScript>(size);
        }

        public PatternQueue()
        {
            _queue = new Queue<SquareScript>();
        }

        public int Count
        {
            get { return _queue.Count; }
        }

        public void Enqueue(SquareScript item)
        {
            _queue.Enqueue(item);
            item.InPattern = true;
            item.PatternIndex = _queue.Count;
            item.EmphasizeColor = EmphasizeColor;
        }

        public SquareScript Dequeue()
        {
            return _queue.Dequeue();
        }
    }
}