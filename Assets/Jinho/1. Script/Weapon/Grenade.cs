using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    public class Grenade : MonoBehaviour
    {
        float damage, raidus;
        Vector3 startPos, endPos;
        Player player;
        ExplosionComponent explosion;
        void Start()
        {
            explosion = GetComponent<ExplosionComponent>();
        }
        public void SetGrenadeData(Vector3 start, Vector3 end, Player player, float radius, float damage = 1)
        {
            startPos = start;
            endPos = end;
            this.player = player;
            this.damage = damage;
            this.raidus = radius;
            StartCoroutine(MoveCo());
        }
        IEnumerator MoveCo()            //포물선의 위치로 날아가는 함수
        {
            float timer = 0;
            while (true)
            {
                timer += Time.deltaTime;
                transform.position = Parabola(startPos, endPos, Vector3.Distance(startPos, endPos) / 2, timer);
                yield return new WaitForEndOfFrame();
            }
        }
        Vector3 Parabola(Vector3 start, Vector3 end, float height, float time)      //포물선 구하는 공식
        {
            Func<float, float> f = x => -4 * height * x * x + 4 * height * x;       //  y = -4ax^2 + 4ax + 0 = f(x)
            var mid = Vector3.Lerp(start, end, time);                                     //mid = x;
            return new Vector3(mid.x, f(time) + Mathf.Lerp(start.y, end.y, time), mid.z);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player) == this.player)
                return;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            explosion.Explosion(this.player.gameObject);
        }
    }
}
