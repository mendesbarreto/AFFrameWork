﻿/****************************************************************************|
/****************************************************************************|
		        *                                                           *
		        *  .=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-.       *
		        *   |                     ______                     |      *
		        *   |                  .-"      "-.                  |      *
		        *   |                 /            \                 |      *
		        *   |     _          |              |          _     |      *
		        *   |    ( \         |,  .-.  .-.  ,|         / )    |      *
		        *   |     > "=._     | )(__/  \__)( |     _.=" <     |      *
		        *   |    (_/"=._"=._ |/     /\     \| _.="_.="\_)    |      *
		        *   |           "=._"(_     ^^     _)"_.="           |      *
		        *   |               "=\__|IIIIII|__/="               |      *
		        *   |              _.="| \IIIIII/ |"=._              |      *
		        *   |    _     _.="_.="\          /"=._"=._     _    |      *
		        *   |   ( \_.="_.="     `--------`     "=._"=._/ )   |      *
		        *   |    > _.="                            "=._ <    |      *
		        *   |   (_/                                    \_)   |      *
		        *   |                                                |      *
		        *   '-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-='      *
		        *                      I warning you:                 *
		        *    Do not touch! Unless you know what you're doing        *
		        *************************************************************/

using System;
using System.Collections.Generic;

using Signals;

using AquelaFrameWork.Input;
using AquelaFrameWork.Sound;
using UnityEngine;

using AquelaFrameWork.Core.Asset;
using AquelaFrameWork.Core.State;

namespace AquelaFrameWork.Core
{
    public class AFEngine : ASingleton<AFEngine> 
    {
        [SerializeField]
        public static readonly string VERSION = "0.0.10";
        [SerializeField]
        public static readonly string FRAME_RATE = "60";
        [SerializeField]
        private bool m_running = true;
        
        protected double m_startTime = 0;
        protected double m_time = 0;
        protected double m_deltaTime = 0;

        public Signal<bool> OnPause = new Signal<bool>();
        public Signal<bool> OnEngineReady = new Signal<bool>();
        public Signal<bool> OnApplicationFocusChange = new Signal<bool>();
        public Signal<bool> OnApplicationExit = new Signal<bool>();
        public Signal<bool> OnApplicationEnable = new Signal<bool>();
        public Signal<NullSignal> OnApplicationDestroy = new Signal<NullSignal>();

        protected AFInput m_input;
        protected AFSoundManager m_soundManager;
        protected AStateManager m_stateManager;

        protected void Awake()
        {
            m_instance = this;
            SetRunning(false);
            Initialize();
        }

        virtual public void ConsoleGetCommand( String name, String paramName )
        {
            //TODO: I have a dream.... Console command working for little test on the AF.
        }

        virtual public void ConsoleSetCommand( String name, String paramName, String value)
        {
            //TODO: I have a dream.... Console command working for little test on the AF.
        }

        virtual public void Destroy()
        {
            //TODO: Make the Engine destroy here
        }

        virtual public void Initialize()
        {
            SetRunning( true );
        }

        virtual public void Pause()
        {
            if(m_running)
            {
                SetRunning( false );
                m_stateManager.Pause();
                OnPause.Dispatch(m_running);
            }
        }

        virtual public void UnPause()
        {
            if (!m_running)
            {
                SetRunning( true );
                m_stateManager.Resume();
                OnPause.Dispatch(m_running);
            }
        }


        public bool GetRunning()
        {
            return m_running;
        }

        private void SetRunning( bool value )
        {
            m_running = value;
        }

        public double GetDeltaTime()
        {
            return m_deltaTime;
        }

        public AFSoundManager GetSoundManager()
        {
            return m_soundManager;
        }

        public AFInput GetInput()
        {
            return m_input;
        }

        virtual protected void Update()
        {
            if( m_running )
            {
                double deltaTime = UnityEngine.Time.smoothDeltaTime;
                //m_input.Update(deltaTime);
                m_stateManager.AFUpdate(deltaTime);
            }
            //Debug.Log("New Resolution: " + AFAssetManager.GetPathTargetPlatformWithResolution());
        }


        // This function is called every fixed framerate frame, if the MonoBehaviour is enabled (Since v1.0)
        void FixedUpdate()
        {

        }

        // LateUpdate is called every frame, if the Behaviour is enabled (Since v1.0)
        void LateUpdate()
        {

        }

        public void ShowConsole()
        {

        }

        public void HideConsole()
        {

        }

        // Sent to all game objects when the player gets or looses focus (Since v3.0)
        void OnApplicationFocus(bool focus)
        {
            OnApplicationFocusChange.Dispatch(focus);
        }

        // Sent to all game objects before the application is quit (Since v1.0)
        void OnApplicationQuit()
        {
            OnApplicationExit.Dispatch(true);
        }

//         void OnApplicationPause(bool pauseStatus)
//         {
//             SetRunning(pauseStatus);
//             OnPause.Dispatch(pauseStatus);
//         }

        // This function is called when the MonoBehaviour will be destroyed (Since v3.2)
        void OnDestroy()
        {
            OnApplicationDestroy.Dispatch();
        }

        // This function is called when the behaviour becomes disabled or inactive (Since v1.0)
        void OnDisable()
        {
            OnApplicationEnable.Dispatch(false);
        }

        // This function is called when the object becomes enabled and active (Since v1.0)
        void OnEnable()
        {
            OnApplicationEnable.Dispatch(false);
        }
    }
}
