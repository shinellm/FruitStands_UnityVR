# FruitStands_UnityVR

Combining VR and Gamification to create a module for my Senior Capstone Thesis:
http://orzo.union.edu/Archives/SeniorProjects/2018/CS.2018/#10

![](Images/Level3-Game-Play.gif)

This module is a self-contained unit of an introductory computer science topic. The module practices the programming concepts of Lists and present them in a gamified VR application. The purpose of this module is to test whether it can improve the academic performance of introductory computer science students. The module was developed using Unity's free VR software, the Oculus Rift Virtual Reality Gaming System, and a Window's workstation.

I am a complete novice at Unity and this project was implemented/programed in 2 months. Below I will describe different information about my final VR project.


## Table of Contents  
[Thesis Abstract](#thesis-abstract)

[Final Module](#final-module)

[Getting Started](#getting-started)

#### How I Developed the Module:
[Oculus Rift Setup](#oculus-rift-setup)

[Unity Environment Setup](#unity-environment-setup)

[Unity Assets Used](#unity-assets-used)

[Unity Scenes Created](#unity-scenes-created)

[Build Settings](#build-settings)

#### Recordings of the Module:

[Gif Gallery](#gif-gallery)

## Thesis Abstract

Virtual reality (VR) and gamification are growing trends that have resulted in the increase of college student motivation, engagement, and educational performance. This report outlines the creation of a module, a self-contained unit of an introductory computer science topic, which combines the use of gamification and VR. The module practices the programming concepts of Lists and present them in a gamified VR application. The purpose of this module is to test whether it can improve the academic performance of introductory computer science students. The experiment described in this paper details the test results taken from two groups of participants, those who were able to use the module and those that watched a video about lists. Since I wanted to ensure that my findings were entirely due to VR's interactive component, both groups of participants completed a 4 question test, that had questions of varying difficulty, before and after finishing their activity. While participants use the module or watch the video, feedback is noted and observational notes describing each participants use of my module and the video are jotted down e.g., how long did they use the module, how many levels did they complete, were they engaged and attentive, and were the confused by the dialogue or task at hand. Based on the data collected, participants that were in the group that watched the video had a larger number of people that were able to correctly answer all of the questions in the final testing section. Therefore, I can only conclude that the module was less beneficial than the video at improving the academic performance of the participant. However, observational data indicates that participants found the VR module more engaging and enjoyable than watching the video.

## Final Module

Due to its ease of translatability to a VR module and engaging preliminary module design, the topic ultimately selected for creation of my module was Lists. The goal of this topic was to create a program that explains to the user how program languages access a specific element of a given list, how it traverses an list, and what happens upon element removal. It also explains that lists have a size, how elements are arranged, out of bound errors, and various other qualities specific to lists. This module is meant for enrichment purposes and reiterates topics that are covered by intro to CS professors and helps to compound the user’s knowledge without explicitly teaching them. To achieve this, I designed a gamified VR module that encompassed a storyline, levels formatted in increasing difficulty, and easy to understand game mechanics. I also decided to implement a grocery oriented storyline hence the name "FruitStands." Volunteers that participated in my final thesis were show the following unity scenes in this order:

  - TestingFood -> OpeningScene_Level1 -> _Demo1 -> OpeningScene_Level2 -> _Demo2 -> OpeningScene_Level4 -> _Demo4 -> Ending

#### Store#1:

![](Images/Store1.gif)

#### Store#2:

![](Images/Store2.gif)

To increase participants familiarity and make it easier for them to understand the Oculus Rift controls, the TestingFood practice scene was created. In the practice scene, the participant is presented with 4 white counters that hold a variety of items that they will later interact with while using the module (e.g. bread, tomato, banana, soda, and chips). They are asked to practice activities such as looking around at their environment, using the left joystick to shift their point of view, using the right joystick to move around, and using the “grip” button to pick up and put down objects. Once they complete the activities mentioned in the module instructions and the participant is ready to move on, we start off in opening scene 1.

The opening scenes help to introduce the participant to the manager who teaches them the different tasks they will complete in the next level. The manager makes a continual appearance to provide a narrative to the module. Similarly to the storyline provided in the initial module design, the participant is still an employee of a fruit store, they stock shelves and serve customers under the guidance of the manager, and end up in a larger grocery store in the final level. Each level teaches the participant a unique concept about lists. In opening scene 1 and level 1, the participant practices basic list assignment and accessing, which is represented by shelves found within the level. They also learn about serving customers. In opening scene 2 and level 2, the participant practices negative indexing and is presented index out of bounds errors. In opening scene 3 and level 3, the participant practices inserting and removing items from multidimensional lists, which is represented by shelves found within the level. In levels 1, 2, and 3, participants were tasked in serving 4 customers using the skills gained from the opening scenes. Throughout the duration of the module notes on the participants questions, comments, body language, and level progression are monitored. Once the participant reached to the ending scene they were thanked for their participation, removed their headset, and were told to wait for further instructions.

## Getting Started

1.  Set up a free unity account and download the software from: https://store.unity.com/

2.  Clone the repo:

        git clone https://github.com/shinellmanwaring/FruitStands_UnityVR.git

3. Open up the Unity application and log in. In the Projects tab select the "Open" option and then select the "FruitStands" folder within your cloned repo folder. It should begin importing the files into Unity.

4. Look in the Assests > FruitStandsLevels folder, select any of the existing scenes, and hit the play button to start playing. 

        NOTE: The scenes are exected to be played as follows: 

        OpeningScene_Level1 -> _Demo1 -> OpeningScene_Level2 -> _Demo2 -> OpeningScene_Level4 -> _Demo4 -> Ending

        The Build Settings should already be configured, but if they aren't, follow the instructions under the 
        ['Build Settings'](#build- settings) header.

## Oculus Rift Setup

- In the oculus box, there is a small pamphlet that tells you to go to this website to help you get started (Took about 40 minutes to       download) : https://www.oculus.com/setup/
- If you need to access the Oculus Rift support website go to: https://support.oculus.com/
- Note: I used a Windows Desktop as my workstation. During my setup, I realized that the sensors couldn't be connected to the USB 3         connection in the back of the computer due to a known Oculus issue. One work around for this is to connected it to the USB port in the     front of the computer tower. If you are connecting another USB into the front of the computer, make sure you don't use the oculus at the   same time because it may cause issues with the oculus connection.
- The oculus headset can be connected to the HDMI and USB 3 connection without issue.

## Unity Environment Setup

- You can set up a free unity account and download the software from: https://store.unity.com/
- Once on the Unity main screen, create a new project and set it as 3D
- When the project loads go to: File > Build Settings > Player Settings > XR Settings. Once there set Virtual Reality Supported to on and   select Oculus.
- When starting you project you'll also need to go to: https://developer.oculus.com/downloads/unity/
- Download the following from the website above: 
  - Oculus Utilities for Unity
  - Oculus Avatar SDK
  - Oculus Platform SDK
- Once downloaded, drag their unity packages into the Assets folder in Unity and import them.
- A video explaining the above can be found here: https://www.youtube.com/watch?v=sxvKGVDmYfY

## Unity Assets Used

After you've downloaded and setup Unity and the Oculus Rift, you can download my FruitStand project from Github and add the desired assests to the assests folder of your newly created project. You may need to download some of the following Unity assets below to fully view my project:
  - Oculus Integration
  - VR Samples
  - TextMesh Pro
  - NavMeshController
  - Food & Grocery Items - Low Poly
  - Simple Shop Interiors - Cartoon assets
  - Character Pack: Free Sample
  - Free Voxel Girl (Dowloaded but ultimately not used)

## Unity Scenes Created

While developing my module, I created the following scenes:
  - Menu (*_Vrsn 1:_* A menu screen that was supposed to be used to load the final module select levels)
  - MainMenu (*_Vrsn 2:_* A menu screen that was supposed to be used to load the final module select levels)
  - VRControls (Used for personal practice using Unity and the Oculus Rift)
  - TestingFood (Used to help participants practice using the Oculus Rift)
  - OpeningScene_Level1 and _Demo1 (Level 1)
  - OpeningScene_Level2 and _Demo2 (Level 2)
  - OpeningScene_Level3 and _Demo3 (Further levels that weren't fully implemented)
  - OpeningScene_Level4 and _Demo4 (Level 3)
  - _Demo5 (Further levels that weren't fully implemented)
  - Ending (The final screen shown to participants that used my module)

## Build Settings

When building my project, you must add the scenes in the proper cronological order to run the module. This can be done by going to: File > Build Settings and then selecting "Add Open Scenes." As you can already guess, this adds the open scene to the "Builds In Scene" list. To play my module you must add the scenes in the following order: 
  - OpeningScene_Level1 -> _Demo1 -> OpeningScene_Level2 -> _Demo2 -> OpeningScene_Level4 -> _Demo4 -> Ending
  
Doing this allows the module to progress through each added scene sequentially.

## Gif Gallery

Here are short snippets of the module in action!

### Some Bugs Encountered & Fixed Along the Way:

#### Apple Bug

![](Images/Apple-Bug.gif)

#### Broccoli Bug

![](Images/Broccoli-Bug.gif)

#### Exploding Shelves

![](Images/Exploding-Shelves.gif)

### Level Examples:

#### Level 1 - Opening Scene

![](Images/Level1-Opening-Scene.gif)

#### Level 3 - Game Play

![](Images/Level3-Game-Play.gif)
