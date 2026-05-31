### ProyectoDinamica3D

# Mini Golf

### Realizado por

**Daniel García Navarro →** @DanielGarciaFlorida

**Isabella McBrown García →** @ismcga 

**Adrià Rodríguez Martínez →** @Adria2304

## Descripción del proyecto

Mini Golf Physics es un juego de minigolf desarrollado cuyo objetivo principal ha sido aplicar conceptos de dinámica, físicas reales y sistemas de colisiones dentro de un entorno 3D.

La mecánica principal consiste en controlar un palo de golf que golpea una bola mediante un sistema de apuntado con ratón y carga de fuerza.

El juego incorpora diferentes obstáculos físicos e interactivos que modifican el comportamiento de la bola durante el recorrido:

**-Molinos de viento.**

**-Trampolines.**

**-Puertas físicas mediante joints.**

**-Obstáculos y colisiones dinámicas.**

También se implementó un sistema visual de trayectoria mediante LineRenderer y una estela dinámica utilizando Trail Renderer para mejorar la percepción visual del movimiento de la bola.


### Instrucciones de juego

El objetivo del juego es introducir la bola en la meta utilizando un único disparo.

El jugador deberá:

1. Apuntar con el ratón para orientar el palo de golf a la dirección deseada de disparo, calculando la fuerza necesaria para alcanzar la meta.
2. Aprovechar los obstáculos físicos del escenario.
3. Utilizar trampolines y molinos para modificar la trayectoria.
4. Reiniciar el nivel si falla el intento.

### Controles

* Mover ratón → apuntar dirección del disparo.
* Mantener clic izquierdo → cargar fuerza.
* Soltar clic izquierdo → realizar golpe.
* Botón Restart → reiniciar el nivel.


## Sistema de golpeo

El sistema de lanzamiento se implementó utilizando físicas reales mediante Rigidbody.

El jugador controla un palo de golf que rota alrededor de la bola siguiendo la posición del ratón sobre el escenario.

Para evitar problemas de rotaciones y orientación del palo, se decidió separar las rotaciones horizontales y verticales utilizando dos pivots independientes:

**PivotY → controla la dirección horizontal.**

**PivotX → controla la animación del swing.**

La fuerza del golpe depende del tiempo que el jugador mantiene pulsado el botón de disparo.

La fuerza se aplica utilizando el modo impulsivo del Rigidbody:

```csharp
rb.AddForce(direction * currentForce, ForceMode.Impulse);
```

La dirección del disparo se obtiene utilizando la orientación horizontal del palo:

```csharp
Vector3 direction = pivotY.forward;
```

El movimiento resultante depende completamente del sistema físico de Unity.


## Sistema de trayectoria

La trayectoria visual se implementó utilizando un LineRenderer combinado con raycasts y proyección del ratón sobre el entorno 3D.

El sistema toma la posición del ratón en pantalla y la proyecta al mundo utilizando un raycast lanzado desde la cámara principal.

Para obtener correctamente la posición sobre el escenario se utiliza un plano horizontal alineado con la bola:

```csharp
Plane planoHorizontal = new Plane(Vector3.up, origenBola);
```

Posteriormente se calcula la dirección entre la bola y la posición del ratón en el mundo.

La trayectoria se representa utilizando un LineRenderer de dos puntos:

* Punto inicial → posición de la bola.
* Punto final → posición del ratón proyectada.

Además, se implementó un sistema de colisión mediante Physics.Raycast().

Permitiendo que la trayectoria se interrumpa visualmente al detectar un obstáculo, proporcionando una retroalimentación visual más realista sobre el entorno y las posibles colisiones.

La trayectoria desaparece automáticamente al realizar el disparo para evitar confusiones visuales durante el movimiento de la bola.


## Sistema de físicas y colisiones

Todo el proyecto se basa en físicas reales utilizando:

*-Rigidbody.*

*-Colliders.*

*-Triggers.*

*-Fuerzas.*

*-Detección de colisiones.*

La bola interactúa físicamente con todos los elementos del escenario:

* Paredes.
* Rampas.
* Obstáculos.
* Molinos.
* Trampolines.
* Puertas físicas.

Para mejorar la estabilidad del movimiento utilizamos:

```csharp
rb.interpolation = RigidbodyInterpolation.Interpolate;
rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
```

También se implementó un límite máximo de velocidad para evitar errores físicos y movimientos exagerados:

```csharp
rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
```


## Molinos de viento

Los molinos de viento funcionan mediante zonas Trigger que aplican fuerzas constantes sobre la bola mientras permanece dentro del área de influencia.

La dirección del viento depende de la orientación del propio objeto. La fuerza se aplica continuamente mediante:

```csharp
ballRigidbody.AddForce(globalWindDirection * windStrength, ForceMode.Force);
```

Esto permite alterar dinámicamente la trayectoria de la bola y generar diferentes rutas dentro del circuito.


## Trampolines

Los trampolines impulsan la bola verticalmente al entrar en contacto con ella.

El sistema se implementó utilizando Trigger Colliders y aplicación de impulsos físicos:

```csharp
ballRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
```

Además, se añadió amortiguación temporal utilizando linearDamping para suavizar el movimiento ascendente y descendente.


## Uso de joints

El proyecto incorpora distintos joints físicos para crear:

* Puertas móviles.
* Obstáculos dinámicos.
* Elementos interactivos.

Estos joints permiten generar escenarios más dinámicos y con interacción física realista.


## Sistema visual

Para reforzar visualmente el movimiento de la bola se implementaron distintos elementos visuales:

* LineRenderer para la trayectoria.
* TrailRenderer para generar una estela dinámica.
* Animación visual del palo de golf.

El Trail Renderer permite visualizar el recorrido realizado por la bola después del disparo.


## Decisiones y soluciones implementadas

Durante el desarrollo surgieron distintos problemas relacionados principalmente con:

* Rotaciones del palo de golf.
* Gestión de pivots.
* Aplicación correcta de fuerzas.
* Trayectoria visual.
* Colisiones físicas.

Para solucionar estos problemas se decidió:

* Separar rotaciones horizontales y verticales mediante pivots independientes.
* Limitar la velocidad máxima de la bola.
* Aplicar fuerzas impulsivas mediante Rigidbody.
* Implementar raycasts para mejorar la visualización de trayectoria.
* Utilizar triggers para los elementos interactivos.

Estas soluciones permitieron conseguir un sistema de juego más estable, visualmente claro y físicamente coherente.

## Enlace Gameplay
https://floridauniversitaria-my.sharepoint.com/:v:/g/personal/adroma01_alumnatflorida_es/IQCvQEuuRu4HQrnFRBwqLUPPAfSxZ9uRjgXwI0BwtkNpF08?nav=eyJyZWZlcnJhbEluZm8iOnsicmVmZXJyYWxBcHAiOiJPbmVEcml2ZUZvckJ1c2luZXNzIiwicmVmZXJyYWxBcHBQbGF0Zm9ybSI6IldlYiIsInJlZmVycmFsTW9kZSI6InZpZXciLCJyZWZlcnJhbFZpZXciOiJNeUZpbGVzTGlua0NvcHkifX0&e=T3udKP
