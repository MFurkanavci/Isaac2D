using UnityEngine;

public class Door : MonoBehaviour
{
    public RoomType doorType;
    public Sprite sprite;

    public GameObject particles;

    public bool isLocked = true;

    public void OpenDoor()
    {
        isLocked = false;

        RunParticles();
    }

    public void RunParticles()
    {
        particles = transform.GetChild(1).gameObject;
        particles.GetComponent<ParticleSystem>().Play();
    }

    public void CloseDoor()
    {
        isLocked = true;
    }

    public RoomType GetDoorType()
    {
        return doorType;
    }

    public void SetDoorType(RoomType _type)
    {
        CloseDoor();
        doorType = _type;
    }

    public void SetSprite(Sprite _sprite)
    {
        sprite = _sprite;
        SpriteRenderer spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = sprite;
            spriteRenderer.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = sprite;
        }
    }

    void OnDisable()
    {
        SetSprite(null);
    }

    void OnEnable()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            if (!isLocked)
            {
                RoomManager.Instance.UseKey();
                RoomManager.Instance.ExitRoom();
                print(GetDoorType() + " door entered");
                RoomManager.Instance.GenerateRoom(GetDoorType());
                print(RoomManager.Instance.GetCurrentRoom() + " room generated");

                if (RoomManager.Instance.GetCurrentRoom().roomType == RoomType.CombatRoom)
                {
                    EnemySpawner.Instance.StartSpawning();
                }
                else if (RoomManager.Instance.GetCurrentRoom().roomType != RoomType.TrapRoom)
                {
                    EventRoomManager.Instance.GenerateEvent();
                }

                isLocked = true;

                player.transform.position = new Vector3(player.transform.position.x, -player.transform.position.y, player.transform.position.z);

            }
        }
    }
}
