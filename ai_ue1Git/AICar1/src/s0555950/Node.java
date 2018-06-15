package s0555950;

import java.awt.Rectangle;

public class Node  {

	Node upNeighbor, downNeighbor, leftNeighbor, rightNeighbor;
	Rectangle rect;
	
	public  void Node(Node upNeighbor,Node downNeighbor,Node leftNeighbor,Node rightNeighbor,Rectangle rect){
	
		this.upNeighbor = upNeighbor;
		this.downNeighbor = downNeighbor;
		this.leftNeighbor = leftNeighbor;
		this.rightNeighbor = rightNeighbor;
	}
	
	public Node getUp(){
		return this.upNeighbor;
	}
	public Node getDown(){
		return this.downNeighbor;
	}
	public Node getLeft(){
		return this.leftNeighbor;
	}
	public Node getRight(){
		return this.rightNeighbor;
	}
}
