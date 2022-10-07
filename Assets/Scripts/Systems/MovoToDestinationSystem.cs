using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

public partial class MovoToDestinationSystem : SystemBase
{    
    /*public void ActivateEntity(Entity entity)
    {
        EntityManager.AddComponent<Active>(entity);
    }

    public void DeActicateEntity(Entity entity)
    {
        EntityManager.RemoveComponent<Active>(entity);
    }*/

    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        Entities.WithAll<Active,MovementSpeed,Destination>()
            .ForEach((ref Translation translation, ref Rotation rotation, in MovementSpeed moveSpeed, in Destination destination) =>
            {
                if (math.all(translation.Value == destination.Value)) { return; }

                float3 toDestination = destination.Value - translation.Value;
                rotation.Value = quaternion.LookRotation(toDestination, new float3(0, 1, 0));
                float3 movement = math.normalize(toDestination) * moveSpeed.Value * deltaTime;
                if (math.length(movement) >= math.length(toDestination))
                {
                    translation.Value = destination.Value;
                }
                else
                {
                    translation.Value += movement;
                }
            }).ScheduleParallel();
    }
}
