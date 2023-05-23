# pgg-ecs-demo
Demonstrate how to generate level with "Procedural Generation Grid" package in ecs way
![image](https://github.com/nicloay/pgg-ecs-demo/assets/1671030/b34d58db-b076-43f1-9a39-f24518e0d266)

# Info
This project require [Procedural Generation Grid (Beta)](https://assetstore.unity.com/packages/tools/utilities/procedural-generation-grid-beta-195535) 
You have to manually import it prior using this demo 

This demo has few limitation
In the MoveToNativeCollection.cs you can see that
1. CellPosition is calculated manually (so it's assume that the grid size is (1,1)
2. It use uniform scale for the prefabs. 

You can adjust the grid size at the build planner executor 
![image](https://github.com/nicloay/pgg-ecs-demo/assets/1671030/5f6baab5-6af0-43d9-bc60-cecaf0c97b71)

# 3D party package usages (they are already installed in teh project).
* project use camera controll from unity entities demo project
* Giphy as FPS on screen counter
* VContainer for dependency injection
* ecs from unity 
