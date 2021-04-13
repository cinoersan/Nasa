
1. Fe is developed with react&typescript and sass. webpack as a bundler.

2. I setup the redux store just to demostrate the way I work, but i didn't need to use in this project, local state was enough..

3. Please run npm install, sometimes a couple of devDependencies might fail to load, at least on my computer. Please install them with the version mentioned in the package.json in that case

4. if any fe dependency load fails you will see as soon as you run npm start, if not webpack will serve the site on port 9000

5. For backend, I didn't use database too much to save time. But there is still a information stored. I removed the migration, you can change connectionstring and run a migration.

6. if you change the ssl port of the backend code, you need to modify src/components/rover/RoverContainer.tsx "the url is hardcoded for time saving"

7. the backend will be served from localhost

8. If everything is okay, you need to upload the csv file. The site logic is  explained in the Nasa.WebApi/Resources/MovementFiles/ScreenShots.pdf

9. CSV contains lines, if you wish to change the plateau size, as mentioned in the document you have to send as the first argument eg:(2,2), but you don't have to, default it starts as (5,5).
   the other lines format as mentioned in the document => 1 2 N|LMLMLMLMM. There are sample files in Nasa.WebApi/Resources/MovementFiles

10. Some controls are added such as "give error if plateau borders are breached", "give error if a crash is going to happen with the given movements".

11. Normally I would write tests for each of the control cases, but to save time I didn't.

12. Normally I would add some css animations. I would put a real rover icon, I send a movement-from variable such as "from-east", I would put this to class of the rover. with "transform: translate" command I would make it seem like coming from east.

13. A real plateau background would look good.

14. I would put ErrorBoundry to FE to handle FE errors.


case => 

A squad of robotic rovers are to be landed by NASA on a plateau on Mars. This plateau, which is curiously rectangular, must be navigated by the rovers so that their on-board cameras can get a complete map of the surrounding terrain to send back to Earth.
A rover's position and location are represented by a combination of x and y coordinates and a letter representing one of the four cardinal compass points. The plateau is divided up into a grid to simplify navigation. An example position might be 0, 0, N, which means the rover is in the bottom left corner and facing North.
In order to control a rover, NASA sends a simple string of letters. The possible letters are 'L', 'R' and 'M'. 'L' and 'R' makes the rover spin 90 degrees left or right respectively, without moving from its current spot. 'M' means move forward one grid point and maintain the same heading.
Assume that the square directly North from (x, y) is (x, y+1).
