#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScreenInteractions
{
    public class ClickManager : IClickManager
    {
        private const int LeftButton = 0;
        
        private readonly Camera _camera;
        
        private readonly List<IClickHandler> _handlers;

        public ClickManager(Camera camera)
        {
            _camera = camera;
            _handlers = new List<IClickHandler>();
        }
        
        public void Update()
        {
            ProcessMouseButton(LeftButton);
        }
        
        void IClickManager.AddHandler(IClickHandler handler)
        {
            if (_handlers.Contains(handler))
            {
                Debug.LogError("ClickManager.AddHandler: handler already added.");
                
                return;
            }
            
            _handlers.Add(handler);
        }

        void IClickManager.RemoveHandler(IClickHandler handler)
        {
            if (!_handlers.Contains(handler))
            {
                Debug.LogError("ClickManager.RemoveHandler: handler not added.");
                
                return;
            }
            
            _handlers.Remove(handler);
        }

        private void ProcessMouseButton(int mouseButton)
        {
            var handlers = _handlers.Where(handler => handler.MouseButton == mouseButton);
            var hit = GetRaycastHit();

            if (Input.GetMouseButtonDown(mouseButton))
            {
                ProcessHandlers(handlers, handler => { handler.Down(hit); });
            }
            else if (Input.GetMouseButton(mouseButton))
            {
                ProcessHandlers(handlers, handler => { handler.Drag(hit); });
            }
            else if (Input.GetMouseButtonUp(mouseButton))
            {
                ProcessHandlers(handlers, handler => { handler.Up(hit); });
            }
        }

        private static void ProcessHandlers(IEnumerable<IClickHandler> handlers, Action<IClickHandler> action)
        {
            foreach (var handler in handlers)
            {
                action.Invoke(handler);
            }
        }

        private RaycastHit? GetRaycastHit()
        {
            var mousePosition = Input.mousePosition;
            var ray = _camera.ScreenPointToRay(mousePosition);

            if (Physics.Raycast(ray, out var hit))
            {
                return hit;
            }

            return null;
        }
    }
}