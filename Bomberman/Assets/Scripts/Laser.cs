using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float defDistanceRay = 0.16f;
    public LineRenderer m_lineRenderer;
    public Transform m_transform;

    public float circleRadius;
    // Start is called before the first frame update
    void Awake()
    {
        m_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootLaser();
    }

    void ShootLaser(){
        if(Physics2D.BoxCast(m_transform.position, new Vector2(1,1), 90 , transform.right)){
            RaycastHit2D _hit = Physics2D.Raycast(m_transform.position,transform.right);
            Draw2DRay(m_transform.position, _hit.point);
        }else{
            Draw2DRay(m_transform.position, m_transform.transform.right * defDistanceRay);
        }
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos) {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }
}
