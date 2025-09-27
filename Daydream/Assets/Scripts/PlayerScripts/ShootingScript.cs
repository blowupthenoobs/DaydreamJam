using Unity.VisualScripting;
using UnityEngine;

public class ShootingScript : PlayerSystem
{
    [SerializeField] GameObject shootPos;
    [SerializeField] GameObject orbitHolder;
    [SerializeField] GameObject bloodProjectile;

    void Update()
    {
        FaceMouse();
        
        if(shootAction.WasPressedThisFrame())
            ShootProjectile();
    }

    private void ShootProjectile()
    {
        GameObject bullet = Instantiate(bloodProjectile, shootPos.transform.position, shootPos.transform.rotation);
    }
    
    private void FaceMouse()
    {
        Vector2 mousePos = mousePosition.ReadValue<Vector2>();
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        orbitHolder.transform.up = direction;
    }
}
