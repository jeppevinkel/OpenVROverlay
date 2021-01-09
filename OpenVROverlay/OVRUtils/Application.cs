using OVRUtils.Exceptions;
using System;
using Valve.VR;

namespace OVRUtils
{
    public class Application
    {
        public enum ApplicationType
        {
            /// <summary>
            /// A 3D application that will be drawing an environment.
            /// </summary>
            Scene = EVRApplicationType.VRApplication_Scene,

            /// <summary>
            /// An application that only interacts with overlays or the dashboard.
            /// </summary>
            Overlay = EVRApplicationType.VRApplication_Overlay,

            /// <summary>
            /// The application will not start SteamVR. If it is not already running
            /// the call with VR_Init will fail with VRInitError_Init_NoServerForBackgroundApp.
            /// </summary>
            Background = EVRApplicationType.VRApplication_Background,

            /// <summary>
            /// The application will start up even if no hardware is present. Only the IVRSettings
            /// and IVRApplications interfaces are guaranteed to work. This application type is
            /// appropriate for things like installers.
            /// </summary>
            Utility = EVRApplicationType.VRApplication_Utility,

            Other = EVRApplicationType.VRApplication_Other
        }

        public readonly ApplicationType Type;
        public readonly CVRSystem OVRSystem;

        /// <summary>
        /// Instantiate and initialize a new <see cref="Application"/>.
        /// Internally, this will initialize the OpenVR API with the specified
        /// <paramref name="type"/> and <paramref name="startupInfo"/>.
        /// </summary>
        /// 
        /// <param name="type"></param>
        /// <param name="startupInfo"></param>
        public Application(ApplicationType type, string startupInfo = "")
        {
            // Attempt to initialize a new OpenVR context
            EVRInitError err = EVRInitError.None;
            OVRSystem = OpenVR.Init(ref err, (EVRApplicationType)type, startupInfo);

            if (err != EVRInitError.None)
                throw new OpenVRSystemException<EVRInitError>("An error occurred while initializing the OpenVR runtime.", err);
        }

        public void Shutdown()
        {
            OpenVR.Shutdown();
        }
    }
}
