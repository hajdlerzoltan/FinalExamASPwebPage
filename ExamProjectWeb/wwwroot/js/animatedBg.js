var width = 4;
var height = 4;
var scene = new THREE.Scene();
var camera = new THREE.OrthographicCamera(
    width / - 2, width / 2, height / 2, height / - 2, 1, 1000);
//camera frustum left, right, top, bottom, near, far

var renderer = new THREE.WebGLRenderer();
//container which holds the scene
var container = document.getElementById("threeContainer");
//containers for relative calculation
var lmao = document.getElementById("htinfo");
var heightObject = document.querySelector('#htinfo').offsetHeight;
var canvaswidth = document.querySelector('#animBg').offsetWidth;
var canvasheight = heightObject;
renderer.setSize(canvaswidth, canvasheight);

container.append(renderer.domElement);

//create the shape
var geometry = new THREE.PlaneGeometry(5, 5);

//create light
let pointLight = new THREE.PointLight(0xccffff, 4, 8, 12);

pointLight.position.x = 1;
pointLight.position.y = 0;
pointLight.position.z = 1;
scene.add(pointLight);
var pos = {
    x: 0,
    y: 0
};
//mouse position
var m = {
    x: 0,
    y: 0
};

// track mouse movement if inside scene, Set light position to cursor
// when cursor leaves set light back to default
lmao.addEventListener('mousemove', onMouseMove, false);
lmao.addEventListener('mouseleave', setLightDefault);

function onMouseMove(event) {

    var bnds = document.getElementById("htinfo").getBoundingClientRect();
    var bnds2 = document.getElementById("animBg").getBoundingClientRect();
    m.x = event.clientX - bnds2.left;
    m.y = event.clientY - bnds.top;
    pos.x = ((m.x / document.querySelector('#animBg').offsetWidth) * 4) - 2;
    pos.y = (((m.y / document.querySelector('#htinfo').offsetHeight) * 4) - 2) * -1;
    //console.log("OFFSET: " + pos.x + " // " + pos.y)
    pointLight.position.set(pos.x, pos.y, 1);

}
function setLightDefault(e) {
    pointLight.position.set(1, 0, 1);
}

//Adjust scene to resized window
window.addEventListener('resize', () => {
    renderer.setSize(
        document.querySelector('#animBg').offsetWidth,
        document.querySelector('#htinfo').offsetHeight
    );
});

//textures
var textureLoader = new THREE.TextureLoader();

var planeNormalMap = textureLoader.load("/images/Normal.png");

//create material
var material = new THREE.MeshPhongMaterial({
    normalMap: planeNormalMap,
    side: THREE.DoubleSide
});
var cube = new THREE.Mesh(geometry, material);
scene.add(cube);
camera.position.z = 3;


//logic
var update = function () {
};

var render = function () {
    renderer.render(scene, camera);

};
var GameLoop = function () {
    requestAnimationFrame(GameLoop);
    update();
    render();

}

GameLoop();