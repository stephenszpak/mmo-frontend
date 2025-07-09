namespace UnityEngine.InputSystem
{
    public class InputAction
    {
        public bool triggered => false;
        public InputAction AddBinding(string binding) => this;
        public InputAction AddCompositeBinding(string composite) => this;
        public InputAction With(string part, string binding) => this;
        public T ReadValue<T>() => default;
    }

    public class InputActionMap
    {
        public InputActionMap(string name) {}
        public InputAction AddAction(string name, string binding = null) => new InputAction();
        public void Enable() {}
    }

    public class Mouse
    {
        public static Mouse current = new Mouse();
        public Button rightButton = new Button();

        public class Button
        {
            public bool isPressed => false;
        }
    }
}
