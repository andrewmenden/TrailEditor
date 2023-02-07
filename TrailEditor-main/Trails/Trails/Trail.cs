using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trails
{
    public class Trail
    {
        public string trailName;
        public string author;
        public List<ILayer> layers;
        
        public Trail(string trailName, string author)
        {
            this.trailName = trailName;
            this.author = author;
            layers = new List<ILayer>();
        }

        public void addLayer(ILayer layer)
        {
            layers.Add(layer);
        }

        public void writeTrail()
        {
            FileStream file = File.Open(trailName + ".trail", FileMode.Create);
            using (BinaryWriter bw = new BinaryWriter(file))
            {
                //first 4 bytes are always the same
                bw.Write(new byte[] { 0x05, 0x00, 0x00, 0x00 });
                bw.Write(trailName);
                bw.Write(author);
                
                //examples of the next 10 bytes in different trails
                //0x00, 0x9F, 0x24, 0x72, 0x8E, 0x72, 0x75, 0xDA, 0x08, 0x04
                //0x00, 0xB5, 0x3F, 0xC6, 0x17, 0xFE, 0x30, 0xD9, 0x08, 0x04
                bw.Write(new byte[] { 0x00 });
                bw.Write(DateTime.Now.ToBinary());
                bw.Write("icon");

                //number of images
                List<string> images = new List<string>();
                for (int i = 0; i < layers.Count; i++)
                {
                    if (layers[i].hasImage())
                    {
                        images.Add(layers[i].image);
                    }
                }
                
                bw.Write(images.Count);

                for (int i = 0; i < images.Count; i++)
                {
                    //twice bc devs suck
                    bw.Write(images[i]);
                    bw.Write(images[i]);
                }

                bw.Write(layers.Count);

                for (int i = 0; i < layers.Count; i++)
                {
                    bw.Write(layers[i].layerType());
                    bw.Write(new byte[] { 0x00 }); //blank for no reason ig
                    bw.Write("Enabled");
                    bw.Write(layers[i].enabled);
                    bw.Write("Order");
                    bw.Write((layers.Count - i - 1).ToString());
                    bw.Write("Layer");
                    bw.Write(layers[i].infrontofplayer ? "TrailInFrontOfLocalPlayersLayer" : "TrailBehindLocalPlayersLayer");
                    bw.Write("Image");
                    if (layers[i].hasImage())
                    {
                        bw.Write(layers[i].image);
                    }
                    else
                    {
                        bw.Write(new byte[] { 0x00 });
                    }
                    bw.Write("Visible");
                    bw.Write(layers[i].ischecked ? "TRUE" : "FALSE");

                    layers[i].writeToFile(bw);
                }
                bw.Write(1);
                bw.Write(0);
                bw.Write(new byte[] { 0x00 });
                bw.Close();
            };
            file.Close();
        }
    }
}
