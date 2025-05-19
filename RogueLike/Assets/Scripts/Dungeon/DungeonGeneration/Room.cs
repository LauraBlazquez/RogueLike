using System.Collections.Generic;
using UnityEngine;


public class Room : MonoBehaviour
{
    public int Width, Height, X, Y;
    public Door up, left, down, right;
    List<Door> doors = new List<Door>();

    void Start()
    {
        if(RoomController.instance == null)
        {
            Debug.Log("Pressed play in the wrong scene!");
            return;
        }
        Door[] ds = GetComponentsInChildren<Door>();
        foreach(Door d in ds)
        {
            doors.Add(d);
            switch (d.doorType)
            {
                case DoorType.up:
                    up = d; 
                break;
                case DoorType.left:
                    left = d;
                break;
                case DoorType.down:
                    down = d;
                break;
                case DoorType.right:
                    right = d;
                break;
            }
        }
        RoomController.instance.RegisterRoom(this);
    }

    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case DoorType.up:
                    door.gameObject.SetActive(GetTop() == null);
                    break;
                case DoorType.right:
                    door.gameObject.SetActive(GetRight() == null);
                    break;
                case DoorType.down:
                    door.gameObject.SetActive(GetBottom() == null);
                    break;
                case DoorType.left:
                    door.gameObject.SetActive(GetLeft() == null);
                    break;
            }
        }
    }

    public Room GetRight()
    {
        return RoomController.instance.DoesRoomExist(X + 1, Y)
            ? RoomController.instance.FindRoom(X + 1, Y)
            : null;
    }

    public Room GetLeft()
    {
        return RoomController.instance.DoesRoomExist(X - 1, Y)
            ? RoomController.instance.FindRoom(X - 1, Y)
            : null;
    }

    public Room GetTop()
    {
        return RoomController.instance.DoesRoomExist(X, Y + 1)
            ? RoomController.instance.FindRoom(X, Y + 1)
            : null;
    }

    public Room GetBottom()
    {
        return RoomController.instance.DoesRoomExist(X, Y - 1)
            ? RoomController.instance.FindRoom(X, Y - 1)
            : null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width,Height, 0));
    }

    public Vector3 GetRoomCentre()
    {
        return new Vector3(X * Width, Y * Height);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }
}
