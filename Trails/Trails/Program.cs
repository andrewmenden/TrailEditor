using Microsoft.VisualBasic.FileIO;
using System;
using System.Runtime.InteropServices;

namespace Trails
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Trail trail = new Trail("test", "Olsu");
            Stripe stripeLayer = new Stripe();
            stripeLayer.lifetime = 3.4f;
            stripeLayer.offset = new Offset(2.4f, 3.4f);
            trail.addLayer(stripeLayer);
            
            trail.writeTrail();
        }
    }
}