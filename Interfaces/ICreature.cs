interface ICreature
{
    // di chuyen
    void Move(float speed);

    // lat mat khi moving
    void Flip();

    // san xuat ra san pham, kieu ga de trung, bo cho sua
    void Production(int id, UnityEngine.Vector3 prodPos);
}
