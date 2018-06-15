package s0555950;

import java.awt.Polygon;
import java.awt.Rectangle;
import java.util.ArrayList;
import java.util.List;

import org.lwjgl.opengl.GL11;
import org.w3c.dom.css.Rect;

import lenz.htw.ai4g.ai.AI;
import lenz.htw.ai4g.ai.DriverAction;
import lenz.htw.ai4g.ai.Info;

public class AI_ue1 extends AI {
	float turningTemp=0;
	float gas, turning, xCar,yCar,xGoal,yGoal;
	List<Polygon> allObst = new ArrayList<Polygon>();
	Rectangle[][] mapGrit = new Rectangle[info.getTrack().getWidth()/10][info.getTrack().getHeight()/10];
	int[][] theWay = new int[info.getTrack().getWidth()/10][info.getTrack().getHeight()/10];

	public AI_ue1(Info info) {
		super(info);
		//enlistForDevelopment();
		enlistForTournament(555950);
		for (Polygon obst : info.getTrack().getObstacles()) {
			allObst.add(obst);
		}
		gridItUp(10);
		
		
	}

	@Override
	public String getName() {
		// TODO Auto-generated method stub
		return "!Baum";
	}

	@Override
	public DriverAction update(boolean arg0) {
		//wenn bool = true dann hat sich pos resetet
		// TODO Auto-generated method stub
		gas = 1;
		xCar = info.getX();
		yCar = info.getY();
		xGoal = info.getCurrentCheckpoint().x;
		yGoal = info.getCurrentCheckpoint().y;
	
		
		//turning to target
		turningTemp= -1* (info.getOrientation()- findTarget());
		if (turningTemp <= -Math.PI ){ 
			turningTemp += 2*Math.PI; 
		}else{
			if( turningTemp >= Math.PI){
				turningTemp -= 2* Math.PI ;	
			}
		}
			
		
		if(getDistance() <=100){
			if (turningTemp >= getDistance()/100 || turningTemp<= -getDistance()/100){
				turning = turningTemp;
			}else
				turning = driveStraigt();
		}else{
			if (turningTemp >= getDistance()/ (getDistance()+200) || turningTemp<= -getDistance()/(getDistance()+200)){
				turning = turningTemp;
			}else
				turning = driveStraigt();
		}
		
		//calculating gas
		gas =  getDistance()/100;
		
		 
		turning += wallDetec();
		
		//cliping the values;	
		 if (gas >= info.getMaxAcceleration()){
			 gas = info.getMaxAcceleration();
		 }
		 if (gas <= -info.getMaxAcceleration()){
			 gas = -info.getMaxAcceleration();
		 }
		 
		if (turning >= info.getMaxAngularAcceleration()) {
			turning = info.getMaxAngularAcceleration();
		}
		if (turning <= -info.getMaxAngularAcceleration()) {
			turning = -info.getMaxAngularAcceleration();
		}
		

		
		return new DriverAction(gas, turning);	
	}
	
	public void getTheWay (){
		
		
	}
	
	//public void 
	
	
	public void gridItUp(int rectSize){
	
		for (int i = 0; i <= info.getTrack().getWidth()-rectSize; i += rectSize) {
			for (int j = 0; j <= info.getTrack().getHeight()-rectSize; j += rectSize) {
				
				 boolean intersect= false;
				 Rectangle	freeRect = new Rectangle(i, j, rectSize, rectSize);		

				
				 for (Polygon pol : allObst) {
					if (pol.intersects(freeRect)){
						intersect= true;
						//System.out.println(freeRect);
						break;
					}
				 }
				 if(!intersect){
					 mapGrit[i/10][j/10]= freeRect;
					 
				 } 
				
			}
		}
	}
	
	public float getDistance(){
		float ret=0;
		
		ret = (float) (Math.sqrt(  Math.pow(  (xGoal-xCar), 2) + Math.pow(yGoal-yCar, 2) ));
		
		return ret;
	}
	
	public float findTarget(){
		//calculates facing
		float ret = 0;
		ret = (float) Math.atan2(yGoal-yCar, xGoal-xCar);
	
		return  ret;
	}
	
	public float wallDetec(){
		float ret = 0;
		float checkDistance = 20;
		float sideDistance = 0.50f;
		float xCheck,yCheck,xCheckR,yCheckR,xCheckL,yCheckL;
		boolean frontCheck, leftCheck, rightCheck;

		xCheck = (float) (xCar + (checkDistance * Math.cos(info.getOrientation())));
		yCheck = (float) (yCar + (checkDistance * Math.sin(info.getOrientation())));
		xCheckR = (float) (xCar + (checkDistance * Math.cos(info.getOrientation() + sideDistance)));
		yCheckR = (float) (yCar + (checkDistance * Math.sin(info.getOrientation() + sideDistance)));
		xCheckL = (float) (xCar + (checkDistance * Math.cos(info.getOrientation() - sideDistance)));
		yCheckL = (float) (yCar + (checkDistance * Math.sin(info.getOrientation() - sideDistance)));
 
		for (Polygon pol : allObst) {
			frontCheck = pol.contains(xCheck, yCheck);
			leftCheck = pol.contains(xCheckL, yCheckL);
			rightCheck =pol.contains(xCheckR, yCheckR);
			
			if(frontCheck && !leftCheck && !rightCheck){
				
				//System.out.println("intersec straight");
				if(info.getOrientation() >= Math.PI/2 || info.getOrientation() <= -Math.PI/2 ){
					ret = -1;					
				}else
					ret = 1;
			}
			 
			if (leftCheck){
				//System.out.println("intersec Left");
				ret = 1; 
			}
			 
			if (rightCheck){
				//System.out.println("intersec Right");
				ret = -1;
			}
			if (rightCheck && leftCheck && !frontCheck){
				//System.out.println("intersec Right/left");
				ret = 0;
			}
		}
		
		return ret;
		
	}
	
	public float driveStraigt (){
		//this methode.. well gets the car in a straigt line
		float ret;
		if (info.getAngularVelocity() != 0){
			ret = 0 - info.getAngularVelocity();
			return  ret;
			}else
				return 0f;
	}
	
	
	
	public String getTextureResourceName  (){
		return "/s0555950/car.png";
	}
	

}
