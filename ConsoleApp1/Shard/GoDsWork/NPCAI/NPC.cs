using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shard.Shard.GoDsWork.NPCAI
{
     class NPC
    {
        public Vector2 Position;
        private Vector2 velocity;
        private Vector2[] waypoints;
        private int currentWaypointIndex = 0;
        private float moveSpeed = 4.0f;
        private float chaseSpeed = 15.0f;
        private float detectionRange = 200.0f;
        private GameObject player;
        private State currentState = State.Patrolling;

        private enum State
        {
            Patrolling,
            Chasing
        }
        public NPC(Vector2 startPosition, Vector2[] patrolWaypoints, GameObject playerReference) 
        {
            Position = startPosition;
            waypoints = patrolWaypoints;
            player = playerReference;
        }

        public void Update(float deltaTime)
        {
            float distanceToPlayer = Position.Distance(new Vector2(player.Transform.Centre.X, player.Transform.Centre.Y));

            if (distanceToPlayer < detectionRange)
            {
                currentState = State.Chasing;
            }
            else if (currentState == State.Chasing && distanceToPlayer > detectionRange * 1.5f) 
            {
                currentState = State.Patrolling;
            }

            if (currentState == State.Chasing)
            {
                ChasePlayer(deltaTime);
            }
            else
            {
                Patrol(deltaTime);
            }
        }

        public void ChasePlayer (float deltaTime)
        {
            MoveTowards(new Vector2(player.Transform.X, player.Transform.Y), chaseSpeed, deltaTime);
        }

        private void MoveTowards(Vector2 target, float speed,  float deltaTime)
        {
            Vector2 direction = (target - Position).Normalize();
            velocity = direction * speed;
            Position = Position + velocity * deltaTime;
        }

        private void Patrol(float deltaTime)
        {
            if (waypoints.Length == 0) return;

            Vector2 target = waypoints[currentWaypointIndex];
            MoveTowards(target, moveSpeed, deltaTime);

            if (Position.Distance(target) < 0.2f) //reached waypoint
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            }
        }
    }
}
