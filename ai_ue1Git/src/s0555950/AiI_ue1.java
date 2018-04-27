package s0555950;

import lenz.htw.ai4g.ai.AI;
import lenz.htw.ai4g.ai.DriverAction;
import lenz.htw.ai4g.ai.Info;

public class AiI_ue1 extends AI {
	float x=0;
	float gas, turning, xCar,yCar,xGoal,yGoal;

	public AiI_ue1(Info info) {
		super(info);
		enlistForDevelopment();
		//info gives lots of informations about position/ velocity/ etc
		// TODO Auto-generated constructor stub
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
		
		
		if(arg0 = true){
			//System.out.println("reset");
		}

		
		
		
		
		
		x= -1* (info.getOrientation()- findTarget());
		if (x <= -Math.PI  ){ 
			x = (float) (2 * Math.PI + x);
		}else{
			if( x >= Math.PI){
				x = (float) ( 2 * Math.PI - x);	
			}
		}
		
		if (x >= 0.4 || x<= -0.4){
		turning = x;
		}else
			turning = driveStraigt();
		
		
		 if (getDistance() <= 5){
			 gas /= 10;
		 }else{
			 //gas /= factor of distance
		 }
		
		
		
			
		if (turning >= info.getMaxAngularAcceleration()) {
			turning = info.getMaxAngularAcceleration();
		}
		if (turning <= -info.getMaxAngularAcceleration()) {
			turning = -info.getMaxAngularAcceleration();
		}
		return new DriverAction(gas, turning);	
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
		return ret;
	}
	
	public float driveStraigt (){
		//this class.. well gets the car in a straigt line
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
