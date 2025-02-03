using UnityEngine;
using System.Collections.Generic;
using OneKnight.Cameras;

namespace OneKnight.OKGraphics {
    public class EllipseRenderer : MonoBehaviour {
        public Ellipse toRender;
        private CameraControllerIS cam;
        private float objSize;
        int limitPoints = 64999;
        public int minPoints = 100;
        public int points;
        public bool pointsDependOnSize;
        
        public float pointsPerPerimeter;
        public Material material;

        private List<Renderer> renderers;
        Color origColor;

        private void Awake() {

            renderers = new List<Renderer>();
        }
        // Use this for initialization
        void Start() {

            origColor = material.GetColor("_Color");
        }

        private void OnEnable() {
            foreach(Renderer renderer in renderers) {
                renderer.enabled = true;
            }
        }

        private void OnDisable() {
            foreach(Renderer renderer in renderers) {
                renderer.enabled = false;
            }
        }

        // Update is called once per frame
        void Update() {
            float zoomFactor = Mathf.Min(1, cam.ZoomFactor/objSize/transform.lossyScale.x);
            for(int i = 0; i < renderers.Count; i++) {
                Material individualMaterial = renderers[i].material;
                individualMaterial.SetColor("_Color", new Color(origColor.r, origColor.g, origColor.b, origColor.a*zoomFactor));
                renderers[i].material = individualMaterial;
            }
        }

        public void Clear() {
            for(int i = 0; i < renderers.Count; i++) {
                Destroy(renderers[i].gameObject);
            }
            renderers.Clear();
            points = minPoints;
        }

        public void SetToRender(Ellipse toRender, CameraControllerIS cam, float objSize) {
            this.cam = cam;
            this.objSize = objSize;
            this.toRender = toRender;
            Clear();
            if(toRender != null) {

                if(pointsDependOnSize) {
                    int temp = (int)(toRender.ApproximatePerimeter*pointsPerPerimeter/objSize);
                    if(temp > points)
                        points = temp;
                }

                for(int i = 0; i < points; i += limitPoints) {

                    int meshIndex = i/limitPoints;
                    GameObject lineSegment = new GameObject("OrbitLine" + i/limitPoints);
                    lineSegment.AddComponent<MeshFilter>();
                    lineSegment.AddComponent<MeshRenderer>();
                    int pointsThisMesh = Mathf.Min(limitPoints, points - i);

                    lineSegment.GetComponent<MeshFilter>().mesh = CreateMesh(meshIndex, pointsThisMesh, limitPoints*(meshIndex));
                    lineSegment.GetComponent<MeshRenderer>().receiveShadows = false;
                    lineSegment.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    lineSegment.transform.parent = transform;
                    lineSegment.transform.localPosition = Vector3.zero;
                    lineSegment.transform.localRotation = Quaternion.Euler(Vector3.zero);
                    //now that they are children, need to compensate with scale (i.e. global scale)
                    lineSegment.transform.localScale = Vector3.one / transform.parent.localScale.x;
                    lineSegment.GetComponent<MeshRenderer>().material = material;
                    lineSegment.tag = OKConstants.IndicatorTag;
                    renderers.Add(lineSegment.GetComponent<MeshRenderer>());
                    lineSegment.GetComponent<MeshRenderer>().enabled = enabled;
                }
            }
        }

        void InstantiateMesh(int meshInd, int nPoints) {
            // Create Mesh
            
        }

        Mesh CreateMesh(int id, int nPoints, int offset) {

            Mesh mesh = new Mesh();

            Vector3[] samples = new Vector3[nPoints+1];
            int[] indices = new int[nPoints+1];
            Color[] colors = new Color[nPoints+1];

            for(int i = 0; i < nPoints+1; i++) {

                indices[i] = i;
                samples[i] = toRender.EvenDistributionPosition((i+offset)*2*Mathf.PI/points);
                colors[i] = Color.white;
            }

            mesh = new Mesh();
            mesh.vertices = samples;
            mesh.SetIndices(indices, MeshTopology.LineStrip, 0);
            mesh.colors = colors;


            return mesh;
        }
    }
}
