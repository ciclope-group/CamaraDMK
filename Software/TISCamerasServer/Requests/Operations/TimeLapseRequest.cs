﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ciclope.Devices.ServerEngine;

namespace Ciclope.Devices.Camera.Server.Requests
{
    public class TimeLapseRequest : Request
    {
        private String camera;
        private String path = null;
        private String image;
        private int frames;
        private int latency;
        private int quality = -1;

        public TimeLapseRequest(String camera, int frames, String path, String name, int latency)
            : base(typeof(void))
        {
            this.camera = camera;
            this.frames = frames;
            this.path = path;
            this.image = name;
            this.latency = latency;
        }

        public TimeLapseRequest(String camera, int frames, String path, String name, int latency, int quality)
            : base(typeof(void))
        {
            this.camera = camera;
            this.frames = frames;
            this.path = path;
            this.image = name;
            this.latency = latency;
            this.quality = quality;
        }

        public TimeLapseRequest(String camera, int frames, String name, int latency)
            : base(typeof(void))
        {
            if (ServerConfig.ImagesPathSet)
            {
                this.camera = camera;
                this.frames = frames;
                this.path = ServerConfig.ImagesPath;
                this.image = name;
                this.latency = latency;
            }
        }

        public TimeLapseRequest(String camera, int frames, String name, int latency, int quality)
            : base(typeof(void))
        {
            if (ServerConfig.ImagesPathSet)
            {
                this.camera = camera;
                this.frames = frames;
                this.path = ServerConfig.ImagesPath;
                this.image = name;
                this.latency = latency;
                this.quality = quality;
            }
        }

        public override Object Attend()
        {
            if (this.path != null)
            {
                TISCameraDriver driver = TISCameraManager.GetReference().getDriver(camera);

                if (this.quality < 0)
                    driver.MakeTimeLapse(frames, path, image, latency);
                else
                    driver.MakeTimeLapse(frames, path, image, latency, quality);

                return null;
            }
            else
                throw new Exception("The path is not defined.");
        }

    }
}
