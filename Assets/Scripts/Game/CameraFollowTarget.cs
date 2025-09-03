using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;   // 이동 속도
    [SerializeField] private float minX = 7f;      // 맵 왼쪽 한계
    [SerializeField] private float maxX = 55f;      // 맵 오른쪽 한계

    private Vector3 dragOrigin;
    private bool isDragging = false;

    void Update()
    {
        HandleKeyboard();
        HandleMouseDrag();
    }

    private void HandleKeyboard()
    {
        if (!GameController.Instance.isPlay) return;

        float horizontal = Input.GetAxisRaw("Horizontal"); // A,D 또는 ←, →
        if (horizontal != 0)
        {
            Vector3 pos = transform.position;
            pos.x += horizontal * moveSpeed * Time.deltaTime;
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            transform.position = pos;
        }
    }

    private void HandleMouseDrag()
    {
        if (!GameController.Instance.isPlay) return;

        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            isDragging = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector3 difference = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
            Vector3 move = new Vector3(-difference.x * moveSpeed, 0, 0);
            transform.Translate(move, Space.World);

            Vector3 pos = transform.position;
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            transform.position = pos;

            dragOrigin = Input.mousePosition;
        }
    }
}
