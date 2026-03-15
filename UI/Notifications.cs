using UnityEngine;
using System.Collections.Generic;
using OneKnight;
using UnityEngine.UI;
using TMPro;

namespace OneKnight.UI {
    public class Notifications : MonoBehaviour {

        public static Notifications world;
        public static Notifications ui;

        public static void CreateNotification(Vector2 position, string text) {
            world.CreateNotif(position, text, world.stdColor);
        }
        public static void CreateNotification(Vector2 position, string text, Color color) {
            world.CreateNotif(position, text, color);
        }
        public static void CreateNotificationKey(Vector2 position, string key) {
            CreateNotificationKey(position, Strings.Get(key));
        }
        public static void CreateNotificationKey(Vector2 position, string key, Color color) {
            CreateNotificationKey(position, Strings.Get(key));
        }

        public static void CreateError(Vector2 position, string text) {
            world.CreateNotif(position, text, world.errorColor);
        }
        public static void CreateErrorKey(Vector2 position, string key) {
            CreateError(position, Strings.Get(key));
        }

        public static void CreateNegative(Vector2 position, string text) {
            world.CreateNotif(position, text, world.negativeColor);
        }
        public static void CreateNegativeKey(Vector2 position, string key) {
            CreateNegativeKey(position, Strings.Get(key));
        }

        public static void CreatePositive(Vector2 position, string text) {
            world.CreateNotif(position, text, world.positiveColor);
        }
        public static void CreatePositiveKey(Vector2 position, string key) {
            CreatePositiveKey(position, Strings.Get(key));
        }

        public static ProgressHint CreateHint(Vector2 position) {
            return CreateHint(position, 0);
        }

        public static ProgressHint CreateHint(Transform follow, Vector2 offset) {
            return CreateHint(follow, offset, 0);
        }

        public static ProgressHint CreateHint(Transform follow, Vector2 offset, float progress) {
            return world._CreateHint(follow, offset, progress);
        }

        public static ProgressHint CreateHelperHint(Vector2 position) {
            return world._ConvertToHelper(CreateHint(position));
        }

        public static ProgressHint CreateHelperHint(Transform follow, Vector2 offset) {
            return world._ConvertToHelper(CreateHint(follow, offset));
        }

        public static ProgressHint CreateHint(Vector2 position, float progress) {
            return world._CreateHint(position, progress);
        }

        public Canvas worldCanvas;
        public bool isUI;
        public ProgressHint hintPrefab;
        public Vector2 littleHintOffset;
        public Vector2 littleHintScale;
        public Text textPrefab;
        public TMP_Text tmpTextPrefab;
        Color DefaultColor {
            get {
                if(tmpTextPrefab != null)
                    return tmpTextPrefab.color;
                return textPrefab.color;
            }
        }
        public Transform defaultPosition;
        public Vector2 stackSpacing;
        public Color errorColor;
        public Color negativeColor;
        public Color stdColor;
        public Color positiveColor;

        public float clearStackDelay = .5f;
        float clearTime;

        Dictionary<Vector2, int> stackCounts;
        // Use this for initialization
        void Awake() {
            if(isUI) {
                ui = this;
            } else {
                world = this;
            }
            stackCounts = new Dictionary<Vector2, int>();
        }

        private void OnDestroy() {
            if(isUI) {
                ui = null;
            } else {
                world = null;
            }
        }
        public void CreateNotif(string text) {
            CreateNotif(defaultPosition.position, text);
        }

        public void CreateNotif(Vector2 position, string text) {
            CreateNotif(position, text, DefaultColor);
        }

        public void CreateNotif(Vector2 position, string text, Color color) {
            if(!stackCounts.ContainsKey(position)) {
                stackCounts[position] = 0;
            }
            InstantiateNotif(position, text, color, stackCounts[position]);
            stackCounts[position] += 1;
        }

        private void InstantiateNotif(Vector2 position, string text, Color color, int stackNumber) {
            clearTime = Time.time + clearStackDelay;
            if(tmpTextPrefab != null) {
                TMP_Text notif = Instantiate(tmpTextPrefab, worldCanvas.transform);
                notif.text = text;
                notif.transform.position = position + stackNumber*stackSpacing;
                notif.color = color;
            } else {
                Text notif = Instantiate(textPrefab, worldCanvas.transform);
                notif.text = text;
                notif.transform.position = position + stackNumber*stackSpacing;
                notif.color = color;
            }
        }

        public ProgressHint _CreateHint(Vector2 position, float progress) {
            ProgressHint hint = Instantiate(hintPrefab, worldCanvas.transform);
            hint.transform.position = position;
            hint.SetProgress(progress);
            return hint;
        }

        public ProgressHint _CreateHint(Vector2 position, float progress, string spriteName, string text) {
            ProgressHint hint = _CreateHint(position, progress);
            Sprite sprite = Resources.Load<Sprite>("Sprites/" + spriteName);
            hint.under.sprite = sprite;
            hint.main.sprite = sprite;
            hint.cornerText.text = text;
            return hint;
        }

        public ProgressHint _CreateHint(Transform follow, Vector2 offset, float progress) {
            ProgressHint result = _CreateHint((Vector2)follow.position + offset, progress);
            result.GetComponent<Follow2D>().follow = follow;
            result.GetComponent<Follow2D>().offset = offset;
            return result;
        }

        private ProgressHint _ConvertToHelper(ProgressHint hint) {
            hint.transform.position = (Vector2)hint.transform.position + littleHintOffset;
            hint.transform.localScale = littleHintScale;
            if(hint.GetComponent<Follow2D>() != null)
                hint.GetComponent<Follow2D>().offset += littleHintOffset;
            return hint;
        }

        // Update is called once per frame
        void LateUpdate() {
            if(Time.time > clearTime)
                stackCounts.Clear();
        }
    }
}