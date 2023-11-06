using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool aim;
		public bool attack;
		public bool knife;
		public bool pistol;
		public bool assault;
		public bool collect;
        public bool grenade;

        [Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnAim(InputValue value)
		{
			AimInput(value.isPressed);
		}

		public void OnAttack(InputValue value)
		{
			AttackInput(value.isPressed);
		}

		public void OnKnife(InputValue value)
		{
			KnifeInput(value.isPressed);
		}

		public void OnPistol(InputValue value)
		{
			PistolInput(value.isPressed);
		}

		public void OnAssault(InputValue value)
		{
			AssaultInput(value.isPressed);
		}

		public void OnCollect(InputValue value)
		{
			CollectInput(value.isPressed);
		}

        public void OnGrenade(InputValue value)
        {
            GrenadeInput(value.isPressed);
        }
#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void AimInput(bool newAimState)
		{
			aim = newAimState;
		}

		public void AttackInput(bool newAttackState)
		{
			attack = newAttackState;
		}

		public void KnifeInput(bool newKnifeState)
		{
			knife = newKnifeState;
		}

		public void PistolInput(bool newPistolState)
		{
			pistol = newPistolState;
		}

		public void AssaultInput(bool newAssaultState)
		{
			assault = newAssaultState;
		}

		public void CollectInput(bool newCollectState)
		{
			collect = newCollectState;
		}

        public void GrenadeInput(bool newGrenadeState)
        {
            grenade = newGrenadeState;
        }

        private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}