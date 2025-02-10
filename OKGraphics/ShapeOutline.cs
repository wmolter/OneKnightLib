using UnityEngine;
using System.Collections.Generic;
using OneKnight.Cameras;

namespace OneKnight.OKGraphics {
    [RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
    public class ShapeOutline : MonoBehaviour {
        public IShape toRender;
        int limitPoints = 64999;
        public int minPoints = 100;
        public bool pointsDependOnSize;
        
        public float pointsPerPerimeter;
        [Range(0, 1)]
        public float thickness = .1f;


        private void Awake() {
            
        }
        // Use this for initialization
        void Start() {
        }

        // Update is called once per frame
        void Update() {
        }

        public void SetToRender(IShape toRender) {
            this.toRender = toRender;
            if(toRender != null) {
                int points = minPoints;
                if(pointsDependOnSize)
                    points = (int)(toRender.ApproximateLength*pointsPerPerimeter);
                points = Mathf.Clamp(points, minPoints, limitPoints);

                if(thickness == 0)
                    GetComponent<MeshFilter>().mesh = CreateLineMesh(points, 0);
                else
                    GetComponent<MeshFilter>().mesh = CreateMesh(points/3, thickness, 0);
                tag = OKConstants.IndicatorTag;
                GetComponent<MeshRenderer>().enabled = enabled;
            }
        }

        Mesh CreateMesh(int nPoints, float thickness, int offset) {
            int vertsPer = 3;
            int vertCount = vertsPer*(nPoints+1);
            Vector3[] samples = new Vector3[vertCount];
            int[] indices = new int[vertCount];
            Color[] colors = new Color[vertCount];

            Vector3 sample = toRender.Sample((offset)*1f/nPoints);
            Vector3 slope = toRender.Sample((offset+.1f)/nPoints) - sample;
            Vector3 perp = new Vector3(slope.y, -slope.x, slope.z).normalized;
            samples[1] = sample + perp*thickness*.5f;
            samples[0] = sample - perp*thickness*.5f;
            sample = toRender.Sample((1+offset)*1f/nPoints);
            slope = toRender.Sample((1+offset+.1f)/nPoints) - sample;
            perp = new Vector3(slope.y, -slope.x, slope.z).normalized*-1;
            samples[2] = sample + perp*thickness*.5f;
            int triangleIndex;
            int sampleIndex;
            int i;
            for(triangleIndex = 1; triangleIndex < nPoints; triangleIndex++) {
                sampleIndex = triangleIndex+1+offset;
                sample = toRender.Sample((sampleIndex)*1f/nPoints);
                slope = toRender.Sample((sampleIndex + .1f)/nPoints) - sample;
                perp = new Vector3(slope.y, -slope.x, slope.z).normalized*Mathf.Sign(triangleIndex%2-1);
                i = vertsPer*triangleIndex;
                samples[i+2] = sample + perp*thickness*.5f;
                samples[i+1] = samples[i-1];
                samples[i] = samples[i-2];
            }
            i = vertsPer*triangleIndex;
            samples[i+2] = sample - perp*thickness*.5f;
            samples[i+1] = samples[i-1];
            samples[i] = samples[i-2];
            for(triangleIndex = 1; triangleIndex < nPoints+1; triangleIndex+=2) {
                i = vertsPer*triangleIndex;
                Vector3 temp = samples[i];
                samples[i] = samples[i+1];
                samples[i+1] = temp;
            }
            for(i = 0; i < vertCount; i++) {
                indices[i] = i;
                colors[i] = Color.white;
            }

            Mesh mesh = new Mesh();
            mesh.vertices = samples;
            mesh.SetIndices(indices, MeshTopology.Triangles, 0);
            mesh.colors = colors;


            return mesh;
        }

        Mesh CreateLineMesh(int nPoints, int offset) {

            Vector3[] samples = new Vector3[nPoints+1];
            int[] indices = new int[nPoints+1];
            Color[] colors = new Color[nPoints+1];

            for(int i = 0; i < nPoints+1; i++) {

                indices[i] = i;
                samples[i] = toRender.Sample((i+offset)*1f/nPoints);
                colors[i] = Color.white;
            }

            Mesh mesh = new Mesh();
            mesh.vertices = samples;
            mesh.SetIndices(indices, MeshTopology.LineStrip, 0);
            mesh.colors = colors;


            return mesh;
        }
    }
}
