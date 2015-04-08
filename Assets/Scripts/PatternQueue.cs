using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class PatternQueue
    {
        private readonly Queue<DotScript> _queue;

        public PatternQueue(int size)
        {
            _queue = new Queue<DotScript>(size);
        }

        public PatternQueue()
        {
            _queue = new Queue<DotScript>();
        }

        public int Count
        {
            get { return _queue.Count; }
        }

        public Color TargetColor { get; set; }

        public void Enqueue(DotScript item)
        {
            _queue.Enqueue(item);
            //item.InPattern = true;
            //item.PatternIndex = _queue.Count;
            //item.TargetColor = TargetColor;
        }

        public DotScript Dequeue()
        {
            return _queue.Dequeue();
        }
    }
}