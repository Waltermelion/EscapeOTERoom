import cv2
import mediapipe as mp
import socket

mp_drawing = mp.solutions.drawing_utils
mp_hands = mp.solutions.hands

# Create a UDP socket
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)

with mp_hands.Hands(static_image_mode=False, max_num_hands=2, min_detection_confidence=0.5) as hands:
  cap = cv2.VideoCapture(0)
  while cap.isOpened():
      success, image = cap.read()
      if not success:
          print("Ignoring empty camera frame.")
          continue

      image = cv2.cvtColor(cv2.flip(image, 1), cv2.COLOR_BGR2RGB)
      results = hands.process(image)

      if results.multi_hand_landmarks:
          for hand_landmarks in results.multi_hand_landmarks:
              mp_drawing.draw_landmarks(image, hand_landmarks, mp_hands.HAND_CONNECTIONS)

      cv2.imshow('MediaPipe Hands', image)
      if cv2.waitKey(5) & 0xFF == 27:
          break

      # Send the results to Unity
      sock.sendto(str(results).encode(), ('localhost', 12345))

cap.release()



