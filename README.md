# Fear Of Heights

Authors: Anis Afridi, Dennis McCarroll, Samuel Rother, Vitaliy Shykhardin

## Installation & Setup
### Installation
### Build
### Deploy to Headset

## Content
### Walkthrough


Scene 0 (Start)
- The user can read the instructions on a UI window and test interactions with grabbable objects.
- The user has to enter a participant ID to start the experiment.


Scene 1 (Relaxation)
- The user is supposed to stand still until the scene changes again.
- Right before the scene changes there will appear a questionnaire which the user has to answer to continue.


Scene 2 (Training Room and Pit Room)
- The user spawns in an explorable training room, where the user can get used to the environment and interactions with the surroundings.
- After a time span of x seconds the door will be highlighted by a green outline and is then ready to be opened by the user walking towards it.
- The user now enters a corridor. If the user continues to walk through it they will be teleported to another corridor, which looks exactly the same, therefore the user won't notice the teleportation.
- The new corridor leads into the Pit Room. In this room the user has to collect a variety of objects from the ground, while being confronted with a deep pit. Once all objects have been collected or after a time pan of x seconds the experiment is over and the user once again has to answer a questionnaire to continue to the next scene.
- If the user falls into the pit during the experiment, they will find themself in a room at the bottom of the pit, similar to the Training Room from the beginning. The user simply can walk through the door follow the corridor, which will teleport the user back up into the corridor outside the Pit Room.


Scene 3 (Relaxation)
- The user is supposed to stand still and relax until another questionnaire pops up, after which the user will be informed of the end of the experiment.

---

### Implementation Details


#### Scene 0 (Start)


UI Start Instructions

- set instruction text in main object "UI Start Instructions", see the object component "Start Introduction Handler" -> "Text Page List"
	- these texts will be set in the "Text Page" child object to be displayed during runtime
- pressing the "Start" button activates the Participant ID UI window


UI Participant ID

- pressing the "OK" button triggers the scene change
- it is not mandatory to enter a participant ID, but highly recommended for data collection (see questionnaires)


Example Objects

- the test objects will appear once the user reaches the third page of the instruction UI which tells the user to test grabbing interactions


---

#### Scene 1 (Relaxation)


Questionnaire Trigger

- set the "Seconds To Wait" parameter in the editor to determine the time span after which the questionnaire will be invoked


Questionnaire

- see the "Questionnaire Handler" script for details on the behaviour of the questionnaire
- to add, delete, or change questions simply enter them in the editor in the object component "Questionnaire Handler" -> "Question List"
- set spawn position in the object component "Position UI"
- after completion the questionnaire will write the answers to a file


---

#### Scene 2 (Training Room)


UI Training Room - UI Instructions TR

- pressing the "Confirm" button starts the timer and which prevents the user from opening the door
	- the timer can be set in the editor in the object component "Open Door (Script)" of the "DoorBody TR" object 


Door TR

- the component "Open Door (Script)" of the child object "DoorBody TR" defines the behavior of the door
- the child object "Trigger Area Door TR" calls several events if the user enters this area
	- the entrance door of the corridor (TR) will be opened
	- the exit door of the corridor (TR) will be opened (cosmetic effect)
	- the entrance door of the corridor leading into the Pit Room will be opened
	- the exit door of the corridor leading into the Pit Room will be opened
	- the outline of the door in the Training Room will be disabled
	- the "Forbidden Area Corridor TR", which renders the screen black, if the walks through the walls or door into the corridor before the timer is up, will be deactivated
	- the "Trigger Area Teleport TR", which upon collision teleports the user to an identical corridor leading into the Pit Room, will be activated


---

#### Scene 2 (Pit Room)


Entrance Door

- the door of the Pit Room has a "Auto Scene Changer" child object, which handles the transition to scene 3
	- its parameter "Time" determines the time span after which the questionnaire will be presented to the user, regardless of the users progress in this scene


Floor

- the "Pit Top Floor" object is part of the "Pit Top Floor" layer, which won't collide with the user (XR Origin -> Character Controller)
	- the player will therefore always fall through the visible surface of the floor in the Pit Room
	- the object "Floor Collider PR" is an invisible box collider, which covers the whole Pit Room Area
		- it can collide with the user and therefore prevent the user from falling trough the floor
		- if the user moves too far over the edge of the pit, a raycast will detect this and the "Floor Collider PR" will be deactivated so the user will fall down


Trigger Area For Entering PR

- the object "Trigger Event For Entering PR" calls several events if the user enters this area
	- the entrance door of the Pit Room will be closed
	- the "Forbidden Area Corridor PR" will be activated
	- the "Forbidden Area PR", which renders the screen black, if the user tries to leave the Pit Room corridor in a forbidden way, will be deactivated
	- the "Trigger Area Teleport PB", which teleports the user from the bottom of the pit back up into the corridor of the Pit Room, will be deactivated
	- the entrance door of the corridor at the bottom of the pit will be closed
	- the raycast, which is checking for a valid floor layer below the users feet, will be activated


Door Pit Bottom

- the child object "Trigger Area Door PB" calls several events if the user enters this area
	- the entrance door of the corridor will be opened
	- the outline of the entrance door will be deactivated
	- the "Forbidden Area Corridor PB" will be deactivated
	- the "Forbidden Area Corridor PR" will be deactivated
	- the "Forbidden Area PR" will be activated
	- the "Trigger Area Teleport PB", which will teleport the user to the upper corridor leading into the Pit Room, will be activated
	- the "Floor Collidor PR" will be actvated, so the user will be able to walk across the floor if the user gets back in the Pit Room


Trigger Area Teleport PB

- if the user enters this area the following events will be called
	- the exit door of the corridor in the pit bottom will be closed
	- the exit door of the corridor leading into the Pit Room will be opened
	- the "Forbidden Area Corridor PB" will be activated
	- the "Forbidden Area PR" will be activated
	- the object "Trigger Area Teleport PB" will deactivate itself


---

#### Scene 3 (Relaxation)


- basically behaves identical to scene 1

---

### Functional Prefabs

Auto Scene Changer
- This Prefab is an functional game object which can be added to a scene in order to automate a scene transition
- For more details see the script "Auto Scene Changer"

Scene Changer Caller
- ... to do

Recenter Point
- ... to do

Trigger Area Event
- This Prefab can be added to a scene to trigger a sequence of events if the user collides with the box collider
- For more details see the script "Collision Triggered Event"

Trigger Area Teleport
- This Prefab can be used to teleport the user upon collision with the box collider
- The user will be teleported to the relative coordinates specified in the editor field "Teleport Vector"
- For more details see the script "Collision Triggered Teleport"

Questionnaire Trigger
- This Prefab is an functional game object which can be added to a scene in order to trigger the questionnaire after a given time x
- For more details see the script "Questionnaire Invoker"

---

### XR Origin
### Editor-scripts
### Known Errors and Warnings

## Used Assets

Imported:
* Floor: [Wooden Floor Material](https://assetstore.unity.com/packages/2d/textures-materials/wood/wooden-floor-materials-150564)  
* Walls & Ceiling: [PBR Materials Sampler Pack](https://assetstore.unity.com/packages/2d/textures-materials/pbr-materials-sampler-pack-40112) 
* Furniture: [Apartment Kit](https://assetstore.unity.com/packages/3d/environments/apartment-kit-124055) 
* Picture inside the window: [Bäume auf Wald mit Sonnenstrahlen](https://unsplash.com/de/fotos/baume-auf-wald-mit-sonnenstrahlen-sp-p7uuT0tw) 
* Interactables: [Low Poly fruit pickups](https://assetstore.unity.com/packages/3d/props/food/low-poly-fruit-pickups-98135) 
* Interactables: [Plates Bowls Mugs Pack](https://assetstore.unity.com/packages/3d/props/interior/plates-bowls-mugs-pack-146682)
* DevModeIndicator-symbol: [Bug png image](https://www.pinterest.de/pin/bug-png-image--584482857867532887/)  
* Oculus VR Hands Implementation: [OculusVRHands](https://github.com/pinglis/OculusVRHands) 
* Oculus VR Hands Model: [Oculus Hand Models](https://developer.oculus.com/downloads/package/oculus-hand-models/)
* Clock: [Clock-Free](https://assetstore.unity.com/packages/3d/props/interior/clock-free-44164)
* Radiator [Two Blades Steel Radiator](https://www.cgtrader.com/free-3d-models/architectural/other/two-blades-steel-radiator-h720-l1110)
* Outline effect: [Quick Outline](https://assetstore.unity.com/packages/tools/particles-effects/quick-outline-115488)
* Door: [Door Free Pack Aferar](https://assetstore.unity.com/packages/3d/props/interior/door-free-pack-aferar-148411)
* Painting: [Picture Frames with photos](https://assetstore.unity.com/packages/3d/props/interior/picture-frames-with-photos-106907)

#### Unused: 
* Floor (used in early stage but removed later): [World Materials Free](https://assetstore.unity.com/packages/2d/textures-materials/world-materials-free-150182)