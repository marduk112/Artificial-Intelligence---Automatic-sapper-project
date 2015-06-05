using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovementAlgorithms.DepthFirstAlgorithm
{
    public class DepthFirstAlgorithm
    {

        public Vert BuildVertGraph(double x, double y, string dir)
        {
            int x1 = Convert.ToInt32(x);
            int y1 = Convert.ToInt32(y);
            Vert root, v1, v2, v3, v4, v5, v6, v7, v8, v9, v10, v11, v12, v13, v14, v15, v16, v17, v18, v19, v20, v21, v22, v23, v24, v25, v26, v27, v28, v29, v30, v31, v32, v33, v34, v35, v36, v37, v38, v39, v40, v41, v42, v43, v44;
            if (dir == "right")
            {
                Console.WriteLine(y1);
                root = new Vert(x1 + 0, y1);
                v2 = new Vert(x1, y1 + 10);
                v3 = new Vert(x1, y1 + 20);
                v4 = new Vert(x1, y1 + 30);
                v5 = new Vert(x1, y1 + 40);
                v6 = new Vert(x1 + 10, y1 + 40);
                v7 = new Vert(x1 + 10, y1 + 20);
                v1 = new Vert(x1 + 10, y1 + 30);
                v8 = new Vert(x1 + 20, y1 + 30);
                v9 = new Vert(x1 + 20, y1 + 40);
                v10 = new Vert(x1 + 30, y1 + 40);
                v11 = new Vert(x1 + 30, y1 + 30);
                v12 = new Vert(x1 + 40, y1 + 40);
                v13 = new Vert(x1 + 40, y1 + 30);
                v14 = new Vert(x1 + 10, y1);
                v15 = new Vert(x1 + 10, y1 + 10);
                v16 = new Vert(x1 + 20, y1 + 10);
                v17 = new Vert(x1 + 20, y1 + 20);
                v18 = new Vert(x1 + 30, y1 + 20);
                v19 = new Vert(x1 + 20, y1);
                v20 = new Vert(x1 + 30, y1 + 10);
                v21 = new Vert(x1 + 40, y1 + 10);
                v22 = new Vert(x1 + 40, y1 + 20);
                v23 = new Vert(x1 + 40, y1);
                v24 = new Vert(x1 + 40, y1);
                v25 = new Vert(x1 + 10, y1 - 10);
                v26 = new Vert(x1 + 20, y1 - 10);
                v27 = new Vert(x1 + 30, y1 - 10);
                v28 = new Vert(x1 + 40, y1 - 10);
                v29 = new Vert(x1 + 40, y1 - 20);
                v30 = new Vert(x1 + 20, y1 - 20);
                v31 = new Vert(x1 + 30, y1 - 20);
                v32 = new Vert(x1 + 0, y1 - 10);
                v33 = new Vert(x1 + 0, y1 - 20);
                v34 = new Vert(x1 + 10, y1 - 20);
                v35 = new Vert(x1 + 10, y1 - 30);
                v36 = new Vert(x1 + 20, y1 - 30);
                v37 = new Vert(x1 + 30, y1 - 30);
                v38 = new Vert(x1 + 40, y1 - 30);
                v39 = new Vert(x1 + 40, y1 - 40);
                v40 = new Vert(x1 + 20, y1 - 40);
                v41 = new Vert(x1 + 30, y1 - 40);
                v42 = new Vert(x1 + 0, y1 - 30);
                v43 = new Vert(x1 + 10, y1 - 40);
                v44 = new Vert(x1 + 0, y1 - 40);
            }
            else
            {
                root = new Vert(x1, y1);
                v2 = new Vert(x1, y1 - 10);
                v3 = new Vert(x1, y1 - 20);
                v4 = new Vert(x1, y1 - 30);
                v5 = new Vert(x1, y1 - 40);
                v6 = new Vert(x1 - 10, y1 - 40);
                v7 = new Vert(x1 - 10, y1 - 20);
                v1 = new Vert(x1 - 10, y1 - 30);
                v8 = new Vert(x1 - 20, y1 - 30);
                v9 = new Vert(x1 - 20, y1 - 40);
                v10 = new Vert(x1 - 30, y1 - 40);
                v11 = new Vert(x1 - 30, y1 - 30);
                v12 = new Vert(x1 - 40, y1 - 40);
                v13 = new Vert(x1 - 40, y1 - 30);
                v14 = new Vert(x1 - 10, y1);
                v15 = new Vert(x1 - 10, y1 - 10);
                v16 = new Vert(x1 - 20, y1 - 10);
                v17 = new Vert(x1 - 20, y1 - 20);
                v18 = new Vert(x1 - 30, y1 - 20);
                v19 = new Vert(x1 - 20, y1);
                v20 = new Vert(x1 - 30, y1 - 10);
                v21 = new Vert(x1 - 40, y1 - 10);
                v22 = new Vert(x1 - 40, y1 - 20);
                v23 = new Vert(x1 - 40, y1);
                v24 = new Vert(x1 - 40, y1);
                v25 = new Vert(x1 - 10, y1 + 10);
                v26 = new Vert(x1 - 20, y1 + 10);
                v27 = new Vert(x1 - 30, y1 + 10);
                v28 = new Vert(x1 - 40, y1 + 10);
                v29 = new Vert(x1 - 40, y1 + 20);
                v30 = new Vert(x1 - 20, y1 + 20);
                v31 = new Vert(x1 - 30, y1 + 20);
                v32 = new Vert(x1 - 0, y1 + 10);
                v33 = new Vert(x1 - 0, y1 + 20);
                v34 = new Vert(x1 - 10, y1 + 20);
                v35 = new Vert(x1 - 10, y1 + 30);
                v36 = new Vert(x1 - 20, y1 + 30);
                v37 = new Vert(x1 - 30, y1 + 30);
                v38 = new Vert(x1 - 40, y1 + 30);
                v39 = new Vert(x1 - 40, y1 + 40);
                v40 = new Vert(x1 - 20, y1 + 40);
                v41 = new Vert(x1 - 30, y1 + 40);
                v42 = new Vert(x1 - 0, y1 + 30);
                v43 = new Vert(x1 - 10, y1 + 40);
                v44 = new Vert(x1 - 0, y1 + 40);
            }

            root.addChild(v2);
            v2.addChild(v3);
            v3.addChild(v4);
            v3.addChild(v7);
            v4.addChild(v5);
            v4.addChild(v6);
            v7.addChild(v1);
            v1.addChild(v8);
            v8.addChild(v9);
            v8.addChild(v11);
            v9.addChild(v10);
            v11.addChild(v12);
            v11.addChild(v13);
            root.addChild(v14);
            v14.addChild(v15);
            v14.addChild(v19);
            v15.addChild(v16);
            v16.addChild(v17);
            v17.addChild(v18);
            v19.addChild(v20);
            v19.addChild(v23);
            v20.addChild(v21);
            v21.addChild(v22);
            v23.addChild(v24);
            root.addChild(v25);
            root.addChild(v32);
            v25.addChild(v26);
            v26.addChild(v27);
            v26.addChild(v30);
            v27.addChild(v28);
            v28.addChild(v29);
            v30.addChild(v31);
            v32.addChild(v33);
            v33.addChild(v34);
            v33.addChild(v42);
            v34.addChild(v35);
            v35.addChild(v36);
            v36.addChild(v37);
            v36.addChild(v40);
            v37.addChild(v38);
            v37.addChild(v39);
            v40.addChild(v41);
            v42.addChild(v43);
            v42.addChild(v44);
            return root;
        }

        private List<Vert> path = new List<Vert>();

        public void Traverse(Vert root)
        {

            this.path.Add(new Vert(root.x, root.y));
            for (int i = 0; i < root.Verticies.Count; i++)
            {

                Traverse(root.Verticies[i]);
                this.path.Add(new Vert(root.x, root.y));

            }
        }

        public List<Vert> getPath()
        {
            return path;
        }

    }
}
