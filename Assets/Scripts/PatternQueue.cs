using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PatternQueue
    {
        private readonly Queue<SquareScript> _queue;

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

        public Color TargetColor { get; set; }

        public void Enqueue(SquareScript item)
        {
            _queue.Enqueue(item);
            item.InPattern = true;
            item.PatternIndex = _queue.Count;
            item.TargetColor = TargetColor;
        }

        public SquareScript Dequeue()
        {
            return _queue.Dequeue();
        }
    }
}