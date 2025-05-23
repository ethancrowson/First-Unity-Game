using TMPro;
using Unity.VisualScripting;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM
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

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;

        public AudioSource GunFireSource;
        public AudioClip GunShotClip;
        private float lastShotTime;
        public float fireRate = 0.2f;

        [SerializeField]
        GameObject bulletPrefab;

        public Transform firePoint; // Empty GameObject at muzzle
        public float fireForce = 25f;

        public GameObject gun;

        public AudioSource GunReloadSource;
        public AudioClip GunReloadClip;
        private bool isReloading = false;

#if ENABLE_INPUT_SYSTEM
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
        public void OnShoot(InputValue value)
        {
            if (value.isPressed && !isReloading && StarterScript.fiveSevenAmmoCount > 0 && Time.time - lastShotTime > fireRate)
            {
				StarterScript.fiveSevenAmmoCount--;
                Animator gunAnim = gun.GetComponent<Animator>();
                gunAnim.SetTrigger("Fire");
                lastShotTime = Time.time;
                GunFireSource.PlayOneShot(GunShotClip);
                var bullet = Instantiate(bulletPrefab, firePoint.position + firePoint.forward * 0.2f, firePoint.rotation);
                bullet.GetComponent<Rigidbody>().angularVelocity = firePoint.forward * fireForce;
				bullet.GetComponent<Rigidbody>().linearDamping = 0.1f;
            }
        }
        public void OnReload(InputValue value)
        {
            if (value.isPressed && !isReloading && StarterScript.fiveSevenAmmoCount < 20)
			{
				isReloading = true;
                Animator gunAnim = gun.GetComponent<Animator>();
				gunAnim.SetTrigger("Reload");
                GunReloadSource.PlayOneShot(GunReloadClip);
            }
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
			if (!isReloading)
			{
				sprint = newSprintState;
			}
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
        public void FinishReload()
        {
            isReloading = false;
            StarterScript.fiveSevenAmmoCount = 20;
        }
    }
	
}