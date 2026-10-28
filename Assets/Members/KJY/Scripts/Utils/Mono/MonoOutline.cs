using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Members.KJY.Scripts.Utils.Mono
{
    public class MonoOutline : MonoBase<Outline>
    {
        public Outline Outline => compo;

        public void SetColor(Color c)
        {
            Outline.effectColor = c;
        }

        public void SetDistance(Vector2 distance)
        {
            Outline.effectDistance = distance;
        }

        public void SetDistanceX(float x)
        {
            Outline.effectDistance.Set(x, Outline.effectDistance.y);
        }

        public void SetDistanceY(float y)
        {
            Outline.effectDistance.Set(Outline.effectDistance.x, y);
        }
    }
}
