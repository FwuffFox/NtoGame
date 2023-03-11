//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/InputSystem/PlayerInputActions.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""InGame"",
            ""id"": ""c85b6ead-dee7-4e90-b0c3-a4dde060706e"",
            ""actions"": [
                {
                    ""name"": ""Pause Button"",
                    ""type"": ""Button"",
                    ""id"": ""c666e558-1716-4476-a1f2-a38887fd65cc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact Button"",
                    ""type"": ""Button"",
                    ""id"": ""25eb8ee9-00de-42f2-84a6-7d15ef3a2555"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""e5ad6e75-774a-4c97-9877-b2e2722825d5"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Run Button"",
                    ""type"": ""Button"",
                    ""id"": ""d4dab112-d349-4596-9211-47ac8afaf798"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Attack Button"",
                    ""type"": ""Button"",
                    ""id"": ""48fcfa9d-919e-44c6-9358-f1442f0472e3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6d3e73d2-fdd8-40c8-bfa9-7ce55aad78c7"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Pause Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bcec009a-f03c-4736-b8b5-028d0caf6bab"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5833a951-e155-4ee6-844e-3aa6d313c49a"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Interact Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""318c22ff-d2cd-4f1a-a82a-b9fd22c40b64"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""d240eccb-0df5-4be0-97fe-331633e3c789"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""d5b8f347-8b2b-4232-ac06-2f69460349d7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""1c429af6-29de-4eac-a1a7-489c2fa30463"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""954c5ad3-2123-48c2-aece-ca4281de41e2"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""e533eca9-0a2c-4f9c-bb1d-951d82c4fc9d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""31ff7b12-9c6b-402e-8100-bc9e693c3d31"",
                    ""path"": ""2DVector(mode=2)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""14820b48-6017-4d46-b646-8cccc765020d"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""705bda7a-8895-429a-979b-ab4c0ce009d9"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""386d76ee-6822-4f71-8a66-f44db5e7733a"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1b2d817a-14b6-4395-a267-b5d54a469fb8"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""bc380996-954c-4204-a6b2-d94085fbe4ee"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Run Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b5007f6-81f7-4938-b79a-93ee3d01f786"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Run Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d1d810e8-a314-431e-b803-69a7581ee85d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard + Mouse"",
                    ""action"": ""Attack Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""31bbfefc-a4d4-4983-9b23-029aecd520db"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Attack Button"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard + Mouse"",
            ""bindingGroup"": ""Keyboard + Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // InGame
        m_InGame = asset.FindActionMap("InGame", throwIfNotFound: true);
        m_InGame_PauseButton = m_InGame.FindAction("Pause Button", throwIfNotFound: true);
        m_InGame_InteractButton = m_InGame.FindAction("Interact Button", throwIfNotFound: true);
        m_InGame_Move = m_InGame.FindAction("Move", throwIfNotFound: true);
        m_InGame_RunButton = m_InGame.FindAction("Run Button", throwIfNotFound: true);
        m_InGame_AttackButton = m_InGame.FindAction("Attack Button", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // InGame
    private readonly InputActionMap m_InGame;
    private IInGameActions m_InGameActionsCallbackInterface;
    private readonly InputAction m_InGame_PauseButton;
    private readonly InputAction m_InGame_InteractButton;
    private readonly InputAction m_InGame_Move;
    private readonly InputAction m_InGame_RunButton;
    private readonly InputAction m_InGame_AttackButton;
    public struct InGameActions
    {
        private @PlayerInputActions m_Wrapper;
        public InGameActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @PauseButton => m_Wrapper.m_InGame_PauseButton;
        public InputAction @InteractButton => m_Wrapper.m_InGame_InteractButton;
        public InputAction @Move => m_Wrapper.m_InGame_Move;
        public InputAction @RunButton => m_Wrapper.m_InGame_RunButton;
        public InputAction @AttackButton => m_Wrapper.m_InGame_AttackButton;
        public InputActionMap Get() { return m_Wrapper.m_InGame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InGameActions set) { return set.Get(); }
        public void SetCallbacks(IInGameActions instance)
        {
            if (m_Wrapper.m_InGameActionsCallbackInterface != null)
            {
                @PauseButton.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnPauseButton;
                @PauseButton.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnPauseButton;
                @PauseButton.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnPauseButton;
                @InteractButton.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnInteractButton;
                @InteractButton.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnInteractButton;
                @InteractButton.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnInteractButton;
                @Move.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnMove;
                @RunButton.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnRunButton;
                @RunButton.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnRunButton;
                @RunButton.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnRunButton;
                @AttackButton.started -= m_Wrapper.m_InGameActionsCallbackInterface.OnAttackButton;
                @AttackButton.performed -= m_Wrapper.m_InGameActionsCallbackInterface.OnAttackButton;
                @AttackButton.canceled -= m_Wrapper.m_InGameActionsCallbackInterface.OnAttackButton;
            }
            m_Wrapper.m_InGameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @PauseButton.started += instance.OnPauseButton;
                @PauseButton.performed += instance.OnPauseButton;
                @PauseButton.canceled += instance.OnPauseButton;
                @InteractButton.started += instance.OnInteractButton;
                @InteractButton.performed += instance.OnInteractButton;
                @InteractButton.canceled += instance.OnInteractButton;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @RunButton.started += instance.OnRunButton;
                @RunButton.performed += instance.OnRunButton;
                @RunButton.canceled += instance.OnRunButton;
                @AttackButton.started += instance.OnAttackButton;
                @AttackButton.performed += instance.OnAttackButton;
                @AttackButton.canceled += instance.OnAttackButton;
            }
        }
    }
    public InGameActions @InGame => new InGameActions(this);
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard + Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    private int m_GamepadSchemeIndex = -1;
    public InputControlScheme GamepadScheme
    {
        get
        {
            if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
            return asset.controlSchemes[m_GamepadSchemeIndex];
        }
    }
    public interface IInGameActions
    {
        void OnPauseButton(InputAction.CallbackContext context);
        void OnInteractButton(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnRunButton(InputAction.CallbackContext context);
        void OnAttackButton(InputAction.CallbackContext context);
    }
}
