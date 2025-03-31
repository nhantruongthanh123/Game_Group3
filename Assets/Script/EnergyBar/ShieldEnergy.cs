using UnityEngine;
using System;
using TMPro;
using System.Collections;
namespace MagicPigGames
{
    [Serializable]
    public class ShieldEnergy : ProgressBar
    {
        public float progress = 0f;
        [SerializeField] private PLayer player;
        private ProgressBar _progressBar;

        public float fillDuration = 10f;
        private float timer = 0f;
        public float wait = 5f;
        private bool isFull = false;
        private bool isPaused = false;

        public TextMeshProUGUI shieldText;
        public KeyCode activationKey = KeyCode.F;

        private Coroutine blinkCoroutine; // Lưu trữ Coroutine nhấp nháy

        protected virtual void Start()
        {
            _progressBar = GetComponent<ProgressBar>();
            if (_progressBar == null)
                Debug.LogError("ProgressBar component is missing!");

            if (shieldText != null)
                shieldText.gameObject.SetActive(false);
        }

        protected virtual void Update()
        {
            if (_progressBar == null) return;

            if (GameManager.Instance != null && GameManager.Instance.isGameOver)
            {
                fillDuration = 10f;
                timer = 0f;
                progress = 0f;
                _progressBar.SetProgress(progress);
                isFull = false;
                isPaused = false;

                if (shieldText != null)
                    shieldText.gameObject.SetActive(false);

                return;
            }

            if (!isFull && !isPaused && player != null && !player.isShieldActive)
            {
                timer += Time.deltaTime;
                progress = Mathf.Clamp01(timer / fillDuration);
                _progressBar.SetProgress(progress);

                if (progress >= 1f)
                {
                    isFull = true;

                    // Nếu chưa chạy Coroutine, bắt đầu nhấp nháy
                    if (blinkCoroutine == null && shieldText != null)
                        blinkCoroutine = StartCoroutine(BlinkText());
                }
            }

            if (isFull && Input.GetKeyDown(activationKey))
            {
                ResetProgress();
            }
        }

        private IEnumerator BlinkText()
        {
            while (isFull)
            {
                shieldText.gameObject.SetActive(!shieldText.gameObject.activeSelf);
                yield return new WaitForSeconds(0.5f); // Nhấp nháy mỗi 0.5 giây
            }
        }

        private void ResetProgress()
        {
            progress = 0f;
            _progressBar.SetProgress(0f);
            timer = 0f;
            isFull = false;
            isPaused = true;

            // Dừng nhấp nháy nếu đang chạy
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
                blinkCoroutine = null;
            }

            if (shieldText != null)
                shieldText.gameObject.SetActive(false);

            Invoke(nameof(ResumeProgress), wait);
        }

        private void ResumeProgress()
        {
            isPaused = false;
        }

        protected override void OnValidate()
        {
            if (_progressBar == null)
                _progressBar = GetComponent<ProgressBar>();
        }
    }
}

