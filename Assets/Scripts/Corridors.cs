using UnityEngine;
using Unity;
public static class Corridors
{


    public static void Generate()
    {
        int max_planes = 100;      //максимально допустимое число блоков
        string planes_turn;         //куда повернул коридор (для дебага)
        int side;                   //генерация стороны
        Vector3 last_plane;         //вектор последнего блока
        Vector3 position = new Vector3(0f,0f,0f);           //позиция для занесения в массив
        float last_plane_z = 0;     //последняя координата по z
        float last_plane_x = 0;     //последняя координата по x
        float x;                    //главная переменная по x
        float z;                    //главная переменная по z
        int last_turn = 0;
        int step = 4;
        Generation.Instance.coordinates.Add(new Coordinate(new Vector3 (0f,0f,0f),"corridor"));
        int i = 1;


        while (i < max_planes)
        {
            int planes_add = Generation.Instance.Chance();                                                        //генерируемое количество добавляемых плейнов
            int add_x = 0;                                                                                             //переменная увеличения координат по x
            int add_z = 0;                                                                                             //переменная увеличения координат по z
            last_plane.x = Generation.Instance.coordinates[Generation.Instance.coordinates.Count - 1].position.x;      //передача переменной координаты x последней точки списка
            last_plane.z = Generation.Instance.coordinates[Generation.Instance.coordinates.Count - 1].position.z;      //передача переменной координаты z последней точки списка

            side = UnityEngine.Random.Range(1, 5);

            switch (side)
            {
                case 0:
                    planes_turn = "0";
                    break;
                case 1:
                    planes_turn = "forward";
                    add_z += step;
                    add_x = 0;
                    break;
                case 2:
                    planes_turn = "right";
                    add_z = 0;
                    add_x += step;
                    break;
                case 3:
                    planes_turn = "back";
                    add_z -= step;
                    add_x = 0;
                    break;
                case 4:
                    planes_turn = "left";
                    add_z = 0;
                    add_x -= step;
                    break;
                default:
                    planes_turn = "0";
                    break;
            };
            for (int j = 0; j < planes_add; j++)
            {
                if (i > max_planes) break;
                if (j >= 3)
                {
                    last_plane_x = Generation.Instance.coordinates[Generation.Instance.coordinates.Count - 1].position.x;
                    last_plane_z = Generation.Instance.coordinates[Generation.Instance.coordinates.Count - 1].position.z;
                }
                last_plane.x += add_x;
                last_plane.z += add_z;
                x = last_plane.x;
                z = last_plane.z;

                if ((side == 1) && (last_plane_z == last_plane.z))
                {
                    side = 0;
                    break;
                }
                if ((side == 2) && (last_plane_x == last_plane.x))
                {
                    side = 0;
                    break;
                }
                if ((side == 3) && (last_plane_z == last_plane.z))
                {
                    side = 0;
                    break;
                }
                if ((side == 4) && (last_plane_x == last_plane.x))
                {
                    side = 0;
                    break;
                }
                if (side == last_turn)
                {
                    last_turn = side;
                    planes_turn = "turn to the same side -" + planes_turn;
                    break;
                }
                //Debug.Log(planes_turn);
                position = new Vector3(x, 0f, z);
                Generation.Instance.coordinates.Add(new Coordinate(position,"corridor"));
                i++;
                last_plane.x = Generation.Instance.coordinates[Generation.Instance.coordinates.Count - 1].position.x;
                last_plane.z = Generation.Instance.coordinates[Generation.Instance.coordinates.Count - 1].position.z;

            }
            last_turn = side;

            if (Generation.Instance.Chance_room() == 1)
            {
                //Debug.Log("Здесь будет комната");
                Rooms.Generate(position);
            }
        }
    }
}
