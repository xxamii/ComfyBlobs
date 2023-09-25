using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    public Transform bottomLeftBorder;
    public Transform topRightBorder;
    public Transform player;

    private Camera _cam;
    private Vector3 _camBL;
    private Vector3 _camTR;
    private Vector3 _camCenter;

    // Start is called before the first frame update
    void Start()
    {
        _cam = Camera.main;
        CalculateCameraBorders();
    }

    private void CalculateCameraBorders()
    {
        Vector3 camBL = _cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 diff = _cam.transform.position - camBL;
        _camBL = bottomLeftBorder.transform.position + diff;
        _camTR = topRightBorder.transform.position - diff;
        _camCenter = (topRightBorder.transform.position + bottomLeftBorder.transform.position) / 2;
    }

    private void CenterCameraOnPlayer()
    {
        Vector3 newPos = new Vector3(player.position.x, player.position.y, _cam.transform.position.z);

        //center or clamp y
        if (_camBL.x > _camTR.x)
        {
            newPos.x = _camCenter.x;
        } else
        {
            if (newPos.x < _camBL.x)
                newPos.x = _camBL.x;
            if (newPos.x > _camTR.x)
                newPos.x = _camTR.x;
        }

        //center or clamp y
        if (_camBL.y > _camTR.y)
        {
            newPos.y = _camCenter.y;
        }
        else
        {
            if (newPos.y < _camBL.y)
                newPos.y = _camBL.y;
            if (newPos.y > _camTR.y)
                newPos.y = _camTR.y;
        }

        _cam.transform.position = newPos;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        if (player == null)
            return;

        CenterCameraOnPlayer();
    }
}
