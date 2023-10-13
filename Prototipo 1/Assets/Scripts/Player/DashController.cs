using System.Collections;
using UnityEngine;

public class DashController : MonoBehaviour
{
    public float dashDistance = 5f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    public string dashButton = "Dash"; // O nome do botão que aciona o dash (defina no Unity Input Manager)

    private Rigidbody2D rb;
    private Vector2 dashDirection;
    private bool isDashing = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(1) && !isDashing)
        {
            StartDash();
        }
    }

    private void StartDash()
    {
        // Defina a direção do dash com base na entrada do jogador (por exemplo, movimento horizontal)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        dashDirection = new Vector2(horizontalInput, verticalInput).normalized;

        if (dashDirection != Vector2.zero)
        {
            StartCoroutine(PerformDash());
        }
    }

    private IEnumerator PerformDash()
    {
        isDashing = true;
        rb.velocity = dashDirection * dashDistance / dashDuration;

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector2.zero;
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
    }
}
