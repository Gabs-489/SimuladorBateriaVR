# 🥁 Simulador de Batería en VR con WiiBoard (Quest 3)

Este es un **demo en realidad virtual** de un simulador de batería desarrollado para **Meta Quest 3**, con soporte opcional para una **WiiBoard** como dispositivo de entrada adicional.  

El proyecto permite experimentar con un **modo libre** de juego, tanto con como sin la WiiBoard conectada.

---

## 📂 Estructura del proyecto

Dentro de la carpeta `Assets` se encuentra:

- `WiiBoard_Codigo.zip`  
  Un proyecto en **C#** que debe compilarse/ejecutarse en PC para poder realizar la lectura de los datos enviados por la WiiBoard.  
  Este código actúa como puente entre la WiiBoard y la aplicación en Unity.

---

## 🔧 Conexión de la WiiBoard

Para conectar la WiiBoard al PC:

1. Abrir **Panel de Control → Dispositivos Bluetooth**.  
2. Buscar y seleccionar la **WiiBoard**.  
3. Durante el emparejamiento aparecerá una solicitud de contraseña.  
   - La WiiBoard **no tiene contraseña**, simplemente selecciona **"Avanzar"**.  
4. Una vez vinculada, ejecutar el proyecto en `WiiBoard_Codigo.zip` para habilitar la lectura de datos.  

⚠️ **Nota:** El demo funciona también **sin la WiiBoard conectada**.

---

## 🥁 Modo de juego

- **Modo libre**:  
  Único modo disponible en el demo. Permite al usuario tocar la batería en VR sin restricciones.

---

## 🚀 Requisitos

- **Meta Quest 3** (para ejecutar el demo en VR).  
- Unity (versión recomendada: la misma en la que se creó el demo).  
- PC con Bluetooth para la conexión de la WiiBoard.  
- (Opcional) WiiBoard conectada por Bluetooth.  

---

## 📌 Notas finales

- Este demo está pensado como una **prueba de concepto** en **Quest 3**.  
- El soporte para WiiBoard es experimental y puede variar según el adaptador Bluetooth del equipo.  
- Aunque se use sin WiiBoard, el modo libre funciona sin problemas.
