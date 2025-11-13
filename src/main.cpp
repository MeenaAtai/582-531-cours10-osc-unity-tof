#include <Arduino.h>
#include <M5_PbHub.h>
#include <MicroOscSlip.h>
#include <FastLED.h>
#include <VL53L0X.h>

VL53L0X  myTOF;

MicroOscSlip<128> monOsc(&Serial);
M5_PbHub myPbHub;

#define BROCHE_ATOM_FIL_BLANC 32 // connecté
#define BROCHE_ATOM_PIXEL 27
CRGB atomPixel;

#define CANAL_ANGLE 0

void setup() {
  Wire.begin();
  myPbHub.begin();
  myTOF.init();

  Serial.begin(115200);

  // Initialiser FastLED pour contrôler le pixel RGB du M5Atom
  FastLED.addLeds<WS2812, 27 , GRB >(&atomPixel, 1); 


  atomPixel = CRGB(255,0,0); // ROUGE
  FastLED.show();
  delay(1000); // PAUSE 1 SECONDE
  atomPixel = CRGB(255,255,0); // JAUNE
  FastLED.show();
  delay(1000); // PAUSE 1 SECONDE
  atomPixel = CRGB(0,255,0); // VERT
  FastLED.show();
  delay(1000); // PAUSE 1 SECONDE
  atomPixel = CRGB(0,0,0);
  FastLED.show(); // PAUSE 1 SECONDE

}

void loop() {
  int maLectureAngle = myPbHub.analogRead(CANAL_ANGLE);
  monOsc.sendInt("/angle", maLectureAngle);

  //exercice - TOF

  int mesure = myTOF.readRangeSingleMillimeters();
  monOsc.sendInt( "/mesure" , mesure);


  delay(50);
}
