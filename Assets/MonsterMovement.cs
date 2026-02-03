using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;

    private int facing = 1; // 1 = вправо, -1 = влево

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float vx = rigid.velocity.x;

        // если скорость слишком маленькая — не трогаем поворот
        if (Mathf.Abs(vx) < 0.5f)
            return;

        int newFacing = vx > 0 ? 1 : -1;

        // поворачиваем ТОЛЬКО если направление реально изменилось
        if (newFacing != facing)
        {
            facing = newFacing;
            spriteRenderer.flipX = facing < 0;
        }
    }
}


//if (Mathf.Abs(vx) > 5f)//Смотрим, движется ли враг по X
//{
//    spriteRenderer.flipX = vx < 0;
//    //spriteRenderer.flipX = vx < 0;
//    //Самая умная строка, но она простая
//    //Сначала разберём:
//    //vx < 0
//    //Это логическое выражение, оно возвращает:
//    //true если vx отрицательное
//    //false если положительное

//    //Читается так:
//    //«Если враг движется влево — отзеркаль спрайт»
//    //Потому что:
//    //влево - отрицательная скорость
//    //вправо - положительная

//    //spriteRenderer.flipX = true; -- эта запись означает:  Отзеркаль спрайт по горизонтали(по оси X)
//}


