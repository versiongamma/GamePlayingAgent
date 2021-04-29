using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COMP717 {
    public class Node {
        public List<Node> children;
        public int value;
        public State state;
        public int[] play;

        public Node(int value, State state, int[] play) {
            this.value = value;
            this.state = state;
            this.children = new List<Node>();
            this.play = play;
        }

        /** Print the Node and it's children to depth 5 */
        public override string ToString() {
            string output = "- " + value + " [" + play[0] + "]"; ;// + " " + state; 

            // Look, I am aware it would definitely be possible to do this programatically, but at
            // this point I don't really want to figure that out just so I can see how my algorithms
            // don't work, cause this is just for me, not for production
            foreach(Node n1 in children) {
                output += "\n\t- " + n1.value + " ["+n1.play[0]+"]";
                //output += "\n" + n1.state;
                foreach(Node n2 in n1.children) {
                    output += "\n\t\t- " + n2.value + " [" + n2.play[0] + "]";
                    //output += "\n" + n2.state;
                    foreach (Node n3 in n2.children) {
                        output += "\n\t\t\t- " + n3.value + " [" + n3.play[0] + "]";
                        //output += "\n" + n3.state;
                        foreach (Node n4 in n3.children) {
                            output += "\n\t\t\t\t- " + n4.value + " [" + n4.play[0] + "]";
                            //output += "\n" + n3.state;
                            foreach (Node n5 in n4.children) {
                                output += "\n\t\t\t\t\t- " + n5.value + " [" + n5.play[0] + "]";
                                //output += "\n" + n3.state;
                                foreach (Node n6 in n5.children) {
                                    output += "\n\t\t\t\t\t\t- " + n6.value + " [" + n6.play[0] + "]";
                                    //output += "\n" + n3.state;
                                }
                            }
                        }
                    }
                }
            }
           
            return output;
        }
    }
}
