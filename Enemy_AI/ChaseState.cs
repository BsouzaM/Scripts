using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IEnemyState
{

    private readonly EnemyState enemy;

    public ChaseState(EnemyState enemyState)
    {
        enemy = enemyState;
    }

    private void Chase()
    {
        Debug.Log("CHASE");
        Vector3 dir = enemy.Target.transform.position - enemy.transform.position; // Direção do inimigo para o player

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg; // Essa linha e a próxima é para a rotação do inimigo
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, q, Time.deltaTime * enemy.ChaseSpeed); // Achei funcionando aqui: http://answers.unity3d.com/questions/650460/rotating-a-2d-sprite-to-face-a-target-on-a-single.html

        if (Vector3.Distance(enemy.transform.position, enemy.Target.transform.position) >= enemy.SlowDownDist)
        {
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.Target.transform.position, enemy.ChaseSpeed * Time.deltaTime); // Move para a direção do player
        }
        else if (Vector3.Distance(enemy.transform.position, enemy.Target.transform.position) < enemy.SlowDownDist && Vector3.Distance(enemy.transform.position, enemy.Target.transform.position) >= enemy.MinDist)
        { // if para distância entre min. distância e distância de desaceleração
            enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, enemy.Target.transform.position, enemy.Speed * Time.deltaTime); // Move para a direção do player, só que agora lento
        }
        else
        {
            PlayerHealth.dead = true; // Se o inimigo se aproximar o suficiente, o jogador é considerado morto.
        }

        if (!PlayerDetectionRay().collider.gameObject.CompareTag("Player"))
        { // Se o guarda não ver o player, volta a patrulhar novamente.
            ToPatrolState();
        }

    }

    public void OnTriggerEnter(Collider other)
    {

    }

    public void ToAlertState()
    {
        enemy.currentState = enemy.alertState;
    }

    public void ToChaseState()
    {

    }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
    }

    public void UpdateState()
    {
        Chase();
        EnemySightLine();
        PlayerDetectionRay();
    }


    private RaycastHit2D PlayerDetectionRay()
    { // A linha do inimigo para o player
        float dist = Vector3.Distance(enemy.Target.transform.position, enemy.transform.position); // Distância do player para o inimigo
        Vector3 dir = enemy.Target.transform.position - enemy.transform.position; // Direção em volta do player
        enemy.Hit = Physics2D.Raycast(new Vector2(enemy.transform.position.x, enemy.transform.position.y),
            new Vector2(dir.x, dir.y), dist, enemy.LayerMask); // Cria um raycast do inimigo para o player
        Debug.DrawRay(enemy.transform.position, dir, Color.red); // Você vai ver uma linha vermelha
        return enemy.Hit;
    }

    private RaycastHit2D EnemySightLine()
    { // Visão do inimigo, bem pequena
        Vector3 sightDir = enemy.transform.TransformDirection(Vector3.right); // Direção ao qual o inimigo olha (para frente)
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(enemy.transform.position.x, enemy.transform.position.y),
            new Vector2(sightDir.x, sightDir.y), 1.0f, enemy.LayerMask); // Cria um raycast para representar a visão do inimigo
        Debug.DrawRay(new Vector2(enemy.transform.position.x, enemy.transform.position.y), new Vector2(sightDir.x, sightDir.y), Color.cyan); // Raio azul
        return hit2;
    }


}
