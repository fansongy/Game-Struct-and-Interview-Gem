using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskCalc : MonoBehaviour {

	// Use this for initialization
	void Start () {
		int[,] data={{1,0},{0,1}};
		Debug.Log(CanFinish(2,data));
		int[,] data2 = {{1,0},{2,0},{3,1}};
		Debug.Log(CanFinish(4,data2));
	}
	
	public bool CanFinish(int numCourses, int[,] prerequisites) {
		//初始化数据
		int[] list = new int[numCourses];
		int[,] matrix = new int[numCourses,numCourses];
		for(int i = 0;i<prerequisites.GetLength(0);++i)
		{
			list[prerequisites[i,0]]++;
			matrix[prerequisites[i,1],prerequisites[i,0]]=1;
		}
		//计算入度
		while(true){
			//找到入度为0的标记
			int start = -1;
			for(int i = 0;i<list.Length;++i)
			{
				if(list[i] == 0){
					start = i;
					list[i] = -1;
					break;
				}
			}
			//未找到入度为0元素，无可完成任务
			if(start == -1)
			{
				break;
			}

			//减少关联节点的入度
			for(int i = 0;i<numCourses;++i)
			{
				if(matrix[start,i] !=0)
				{
					list[i]--;    
				}
			}
		}

		//检查任务完成情况
		for(int i = 0;i<numCourses;++i){
			if(list[i] != -1){
				return false;
			}
		}
		return  true;
	}
}
