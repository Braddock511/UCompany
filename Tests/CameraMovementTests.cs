using NUnit.Framework;
using UnityEngine;

[TestFixture]
public class CameraMovementTests
{
    [Test]
    public void Zoom_IncreaseZoom_WithinBounds()
    {
        CameraMovement cameraMovement = new GameObject().AddComponent<CameraMovement>();
        Camera cameraComponent = cameraMovement.gameObject.AddComponent<Camera>();
        cameraMovement.cam = cameraComponent;
        cameraMovement.zoomSpeed = 1.0f;
        cameraMovement.maxZoom = 10.0f;

        cameraMovement.Zoom(Input.GetAxis("Mouse ScrollWheel"));

        Assert.Greater(cameraComponent.orthographicSize, 2.5f);
        Assert.LessOrEqual(cameraComponent.orthographicSize, 10.0f);
    }

    [Test]
    public void MoveCamera_DragMouse_MoveCameraWithinBounds()
    {
        CameraMovement cameraMovement = new GameObject().AddComponent<CameraMovement>();
        Camera cameraComponent = cameraMovement.gameObject.AddComponent<Camera>();
        cameraMovement.cam = cameraComponent;
        cameraMovement.mapRenderer = new GameObject().AddComponent<SpriteRenderer>();
        cameraMovement.isActive = true;

        cameraMovement.MoveCamera();
        
        Assert.IsNotNull(cameraMovement.dragOrigin);
    }

    [Test]
    public void ClampCam_TargetPosition_ClampedPositionWithinBounds()
    {
        CameraMovement cameraMovement = new GameObject().AddComponent<CameraMovement>();
        Camera cameraComponent = cameraMovement.gameObject.AddComponent<Camera>();
        cameraMovement.cam = cameraComponent;
        cameraMovement.mapRenderer = new GameObject().AddComponent<SpriteRenderer>();
        cameraMovement.isActive = true;

        Vector3 targetPosition = new Vector3(10.0f, 5.0f, 0.0f);

        Vector3 clampedPosition = cameraMovement.ClampCam(targetPosition);

        Assert.LessOrEqual(clampedPosition.x, cameraMovement.mapMaxX - cameraComponent.orthographicSize * cameraComponent.aspect);
        Assert.GreaterOrEqual(clampedPosition.x, cameraMovement.mapMinX + cameraComponent.orthographicSize * cameraComponent.aspect);
        Assert.LessOrEqual(clampedPosition.y, cameraMovement.mapMaxY - cameraComponent.orthographicSize);
        Assert.GreaterOrEqual(clampedPosition.y, cameraMovement.mapMinY + cameraComponent.orthographicSize);
    }
}
