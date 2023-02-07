using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Trails
{
    public struct Color //white is 1,1,1
    {
        public float r,g,b;

        public Color(float r, float g, float b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
    }

    public struct Offset
    {
        public float x, y;

        public Offset(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public struct Int2D
    {
        public int x, y;
        public Int2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public interface ILayer
    {
        public bool ischecked { get; set; }
        public string enabled { get; set; } //never, always, only at superspeed, not at superspeed
        public string image { get; set; }
        public Color color { get; set; } //values between 0 and 1
        public float opacity { get; set; } //between 0 and 1
        public bool samesideup { get; set; }

        public bool infrontofplayer { get; set; }

        public bool hasImage()
        {
            if (image == string.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public int layerType();
        public void writeToFile(BinaryWriter bw);
    }

    public class Stripe : ILayer
    {
        #region AnnoyingStuffGoByeBye
        public int layerType() { return 0x1700; }

        public bool ischecked { get; set; }
        public string enabled { get; set; } //never, always, only at superspeed, not at superspeed
        public string image { get; set; }
        public Color color { get; set; } //values between 0 and 1
        public float opacity { get; set; } //between 0 and 1
        public bool samesideup { get; set; }
        public bool infrontofplayer { get; set; }

        public float lifetime { get; set; } //0.1 through 100
        public bool taper {get; set;}
        public bool fadeout {get; set;}
        public float fadeoutspeed {get; set;} //0.1 through 100
        public float size {get; set;} //1 through 200
        public Offset offset {get; set;} // -100 through 100
        public bool invertoffset {get; set;}
        public bool fliphorizontally {get; set;}
        public bool flipvertically {get; set;}
        public float noise {get; set;} //0 through 100
        public float waveamplitude {get; set;} //0 through 100
        public float wavefrequency {get; set;} //-100 through 100
        public float wavephaseoffset {get; set;} //-pi through pi

        public Stripe()
        {
            ischecked = true;
            enabled = "NEVER";
            image = string.Empty;
            color = new Color(1, 1, 1);
            opacity = 1;
            samesideup = false;
            infrontofplayer = false;

            lifetime = 2;
            taper = false;
            fadeout = false;
            fadeoutspeed = 1;
            size = 40;
            offset = new Offset(0, 0);
            invertoffset = true;
            fliphorizontally = false;
            flipvertically = false;
            noise = 0;
            waveamplitude = 0;
            wavefrequency = 0;
            wavephaseoffset = 0;
        }
        #endregion
    
        public void writeToFile(BinaryWriter bw)
        {
            bw.Write("LifeTime");
            bw.Write(this.lifetime.ToString());
            bw.Write("Color");
            bw.Write(this.color.r.ToString() + "," + this.color.g.ToString() + "," + this.color.b.ToString());
            bw.Write("Taper");
            bw.Write(this.taper ? "TRUE" : "FALSE");
            bw.Write("FadeOut");
            bw.Write(this.fadeout ? "TRUE" : "FALSE");
            bw.Write("FadeOut Speed");
            bw.Write(this.fadeoutspeed.ToString());
            bw.Write("Opacity");
            bw.Write(this.opacity.ToString());
            bw.Write("Size");
            bw.Write(this.size.ToString());
            //more bad devs
            bw.Write("Offset");
            bw.Write("0");
            bw.Write("X-Offset");
            bw.Write("0");
            //senior obviously walked in and was like "do this instead"
            bw.Write("OffsetVector");
            bw.Write(this.offset.x.ToString()+","+ this.offset.y.ToString());
            bw.Write("Invert Offset");
            bw.Write(this.invertoffset ? "TRUE" : "FALSE");
            bw.Write("Flip Horizontally");
            bw.Write(this.fliphorizontally ? "TRUE" : "FALSE");
            bw.Write("Flip Vertically");
            bw.Write(this.flipvertically ? "TRUE" : "FALSE");
            bw.Write("Force right side Up"); //naming inconsistency
            bw.Write(this.samesideup ? "TRUE" : "FALSE");
            bw.Write("Noise");
            bw.Write(this.noise.ToString());
            bw.Write("Sinewave Amplitude"); //more naming inconsistencies
            bw.Write(this.waveamplitude.ToString());
            bw.Write("Sinewave Frequency");
            bw.Write(this.wavefrequency.ToString());
            bw.Write("Sine Phase Offset");
            bw.Write(this.wavephaseoffset.ToString());
        }
    }

    public class Particle : ILayer
    {
        #region AnnoyingStuffGoByeBye
        public int layerType() { return 0x2301; }

        public bool ischecked { get; set; }
        public string enabled { get; set; } //never, always, only at superspeed, not at superspeed
        public string image { get; set; }
        public Color color { get; set; } //values between 0 and 1
        public float opacity { get; set; } //between 0 and 1
        public bool samesideup { get; set; }
        public bool infrontofplayer { get; set; }

        public string spritemode {get; set;} //DEFAULT, ANIMATED, RANDOM, SEQUENTIAL
        public Int2D spritecount {get; set;}
        public float animationfps {get; set;}
        public float spawninterval {get; set;}
        public int amount {get; set;}
        public float lifetime {get; set;}
        public bool fadeout {get; set;}
        public float scale {get; set;}
        public float scalespeed {get; set;}
        public float scalevariance {get; set;}
        public float rotation {get; set;}
        public float rotationvariance {get; set;}
        public float rotationspeed {get; set;}
        public float rotationspeedvariance {get; set;}
        public bool rotatewithplayer {get; set;}
        public Offset spawnoffset {get; set;}
        public Offset spawnoffsetvariance {get; set;}
        public float force {get; set;}
        public float forcevariance {get; set;}
        public Offset direction {get; set;}
        public Offset directionvariance {get; set;}
        public bool useworldaxis {get; set;}
        public Offset gravity { get; set; }
    
        public Particle()
        {
            ischecked = true;
            enabled = "NEVER";
            image = string.Empty;
            color = new Color(1, 1, 1);
            opacity = 1;
            samesideup = false;
            infrontofplayer = false;

            spritemode = "DEFAULT";
            spritecount = new Int2D(1, 1);
            animationfps = 30;
            spawninterval = 0.25f;
            amount = 1;
            lifetime = 1;
            fadeout = true;
            scale = 1;
            scalespeed = -1;
            scalevariance = 0.5f;
            rotation = 0;
            rotationvariance = 0;
            rotationspeed = 0;
            rotationspeedvariance = 0;
            rotatewithplayer = false;
            spawnoffset = new Offset(0, 0);
            spawnoffsetvariance = new Offset(0, 0);
            force = 0;
            forcevariance = 0;
            direction = new Offset(1, 0);
            directionvariance = new Offset(0, 0);
            useworldaxis = false;
            gravity = new Offset(0, 0);
        }
        #endregion

        public void writeToFile(BinaryWriter bw)
        {
            bw.Write("isAnimated");
            bw.Write("FALSE"); //always
            bw.Write("spriteMode");
            bw.Write(spritemode);
            bw.Write("SpriteSize"); //could be an error
            bw.Write("100,100");
            bw.Write("SpriteCount");
            bw.Write(spritecount.x.ToString() + "," + spritecount.y.ToString());
            bw.Write("FPS");
            bw.Write(animationfps.ToString());
            bw.Write("Spawn Rate");
            bw.Write(spawninterval.ToString());
            bw.Write("Amount");
            bw.Write(amount.ToString());
            bw.Write("LifeTime");
            bw.Write(lifetime.ToString());
            bw.Write("FadeOut");
            bw.Write(fadeout ? "TRUE" : "FALSE");
            bw.Write("Scale");
            bw.Write(scale.ToString());
            bw.Write("ScaleSpeed");
            bw.Write(scalespeed.ToString());
            bw.Write("Scale Variance");
            bw.Write(scalevariance.ToString());
            bw.Write("Rotation");
            bw.Write(rotation.ToString());
            bw.Write("Rotation Variance");
            bw.Write(rotationvariance.ToString());
            bw.Write("Rotation Speed");
            bw.Write(rotationspeed.ToString());
            bw.Write("Rotation Speed Variance");
            bw.Write(rotationspeedvariance.ToString());
            bw.Write("Rotate with Player");
            bw.Write(rotatewithplayer ? "TRUE" : "FALSE");
            bw.Write("Color");
            bw.Write(color.r.ToString() + "," + color.g.ToString() + "," + color.b.ToString());
            bw.Write("Opacity");
            bw.Write(opacity.ToString());
            bw.Write("Offset");
            bw.Write(spawnoffset.x.ToString() + "," + spawnoffset.y.ToString());
            bw.Write("OffsetVariance");
            bw.Write(spawnoffsetvariance.x.ToString() + "," + spawnoffsetvariance.y.ToString());
            bw.Write("Force");
            bw.Write(force.ToString());
            bw.Write("Force Variance");
            bw.Write(forcevariance.ToString());
            bw.Write("Direction");
            bw.Write(direction.x.ToString() + "," + direction.y.ToString());
            bw.Write("Direction Variance");
            bw.Write(directionvariance.x.ToString() + "," + directionvariance.y.ToString());

            bw.Write("Use World Axis"); //come back to this!!
            bw.Write(samesideup ? "TRUE" : "FALSE");
            bw.Write("Same Side Up");
            bw.Write(samesideup ? "TRUE" : "FALSE");
            bw.Write("hasGravity"); //come back to this aswell
            bw.Write("FALSE");

            bw.Write("gravity");
            bw.Write(gravity.x.ToString() + "," + gravity.y.ToString());

            bw.Write("Is Beta Trail"); //also check this
            bw.Write("FALSE");
        }
    }

    public class Animation : ILayer
    {
        #region AnnoyingStuffGoByeBye
        public int layerType() { return 0x1402; }

        public bool ischecked { get; set; }
        public string enabled { get; set; } //never, always, only at superspeed, not at superspeed
        public string image { get; set; }
        public Color color { get; set; } //values between 0 and 1
        public float opacity { get; set; } //between 0 and 1
        public bool samesideup { get; set; }
        public bool infrontofplayer { get; set; }

        public Int2D spritecount {get; set;}
        public byte startframe {get; set;}
        public byte endframe {get; set;}
        public float animationfps {get; set;}
        public string loop {get; set; } //LOOP, PING PONG, ONCE THEN FREEZE, ONCE THEN DISAPPEAR
        public Offset offset {get; set;}
        public float scale {get; set;}
        public float fadein {get; set;}
        public float fadeout {get; set;}
        public float scaleout {get; set;}
        public bool rotatewithplayer {get; set;}
        public bool movewheninactive { get; set; }

        public Animation()
        {
            ischecked = true;
            enabled = "NEVER";
            image = string.Empty;
            color = new Color(1, 1, 1);
            opacity = 1;
            samesideup = false;
            infrontofplayer = false;

            spritecount = new Int2D(1, 1);
            startframe = 0;
            endframe = 0;
            animationfps = 30;
            loop = "LOOP";
            offset = new Offset(0, 0);
            scale = 1;
            fadein = 0;
            fadeout = 0;
            scaleout = 0;
            rotatewithplayer = true;
            movewheninactive = true;
        }
        #endregion

        public void writeToFile(BinaryWriter bw)
        {
            bw.Write("SpriteCount");
            bw.Write(spritecount.x.ToString() + "," + spritecount.y.ToString());
            bw.Write("Start frame");
            bw.Write(startframe.ToString());
            bw.Write("End frame");
            bw.Write(endframe.ToString());
            bw.Write("FPS");
            bw.Write(animationfps.ToString());
            bw.Write("Loop");
            bw.Write(loop);
            bw.Write("Color");
            bw.Write(color.r.ToString() + "," + color.g.ToString() + "," + color.b.ToString());
            bw.Write("Opacity");
            bw.Write(opacity.ToString());
            bw.Write("Offset");
            bw.Write(offset.x.ToString() + "," + offset.y.ToString());
            bw.Write("Scale");
            bw.Write(scale.ToString());
            bw.Write("FadeIn");
            bw.Write(fadein.ToString());
            bw.Write("FadeOut");
            bw.Write(fadeout.ToString());
            bw.Write("ScaleOut");
            bw.Write(scaleout.ToString());
            bw.Write("Force right side Up"); //check this
            bw.Write(samesideup ? "TRUE" : "FALSE");
            bw.Write("Rotate with Player");
            bw.Write(rotatewithplayer ? "TRUE" : "FALSE");
            bw.Write("Move when inactive");
            bw.Write(movewheninactive ? "TRUE" : "FALSE");
        }
    }
}
