using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum FiringTypes { Single, Burst, Cone, BolasShot, Bomb }

public class Shooter : MonoBehaviour
{
    //General Settings
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] FiringTypes firingType; // This can be used for player powerups as well
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFiringRate = .2f;

    //===Firing Type Settings===
    //Burst Fire
    [SerializeField] int projectilesPerBurst = 3;
    [SerializeField] float timeBetweenBurstShots = .5f;

    //Cone Fire
    [SerializeField] [Range(.1f,.5f)]float coneSpread = .25f;
    [SerializeField] float projectileRotateSpeed = 20f;
    

    // AI Settings
    public bool isAI = false;
    
    [SerializeField] float firingRateVariance = 0f;
    [SerializeField] float minimumFiringRate = .1f;

    
    public bool isFiring = false;
    bool coneProjectileRotation = false;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (isAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    private void RotateProjectile(Rigidbody2D rb)
    {
        ProjectileRotator rotator = rb.GetComponent<ProjectileRotator>();
        rotator.TurnRoationOn(projectileRotateSpeed);
    }

    private void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            if(firingType == FiringTypes.Single)
            {
                firingCoroutine = StartCoroutine(SingleFire());
            }
            else if(firingType == FiringTypes.Burst)
            {
                firingCoroutine= StartCoroutine(BurstFire());
            }
            else if(firingType == FiringTypes.Cone)
            {
                firingCoroutine = StartCoroutine(ConeFire());
            }
            //Else if other types
                
        }    
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator SingleFire()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            
            Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

            if(rb != null)
            {
                rb.velocity = transform.up * projectileSpeed;
            }

            Destroy(instance, projectileLifetime);
            
            audioPlayer.PlayShootingClip();
            
            yield return new WaitForSeconds(TimeToNextShot());
        }
    }

    float TimeToNextShot()
    {
        float timeToNextProjectile = Random.Range(baseFiringRate - firingRateVariance,
                                                    baseFiringRate + firingRateVariance);
        return Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);
    }

    IEnumerator BurstFire()
    {
        while (true)
        {
            for (int i = 0; i < projectilesPerBurst; i++)
            {
                GameObject instance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

                Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    rb.velocity = transform.up * projectileSpeed;
                }

                Destroy(instance, projectileLifetime);

                yield return new WaitForSeconds(timeBetweenBurstShots);
            }
            yield return new WaitForSeconds(TimeToNextShot());
        }
    }

    IEnumerator ConeFire()
    {
        while (true)
        {
            GameObject inst1 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            GameObject inst2 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            GameObject inst3 = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

            Rigidbody2D rb1 = inst1.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = inst2.GetComponent<Rigidbody2D>();
            Rigidbody2D rb3 = inst3.GetComponent<Rigidbody2D>();

            if (rb1 != null)
            {
                RotateProjectile(rb1);
                RotateProjectile(rb2);
                RotateProjectile(rb3);
                rb1.velocity = transform.up * projectileSpeed;
                rb2.velocity = (transform.up + -transform.right * coneSpread) * projectileSpeed;
                rb3.velocity = (transform.up + transform.right * coneSpread) * projectileSpeed;
            }

            Destroy(inst1, projectileLifetime);
            Destroy(inst2, projectileLifetime);
            Destroy(inst3, projectileLifetime);

            audioPlayer.PlayShootingClip();

            yield return new WaitForSeconds(TimeToNextShot());
        }
    }

    void LargeShotFire()
    {

    }

    void BombFire()
    {

    }

    public FiringTypes GetFiringType()
    {
        return firingType;
    }

    public bool GetIsAI()
    {
        return isAI;
    }
}

[CustomEditor(typeof(Shooter))]
public class ShooterScriptEditor : Editor
{
    Shooter shooterScript;
    
    SerializedProperty projectilePrefab;
    SerializedProperty firingType;
    SerializedProperty projectileSpeed;
    SerializedProperty projectileLifetime;
    SerializedProperty baseFiringRate;

    SerializedProperty projectilesPerBurst;
    SerializedProperty timeBetweenBurstShots;

    SerializedProperty coneSpread;
    SerializedProperty projectileRotateSpeed;

    SerializedProperty firingRateVariance;
    SerializedProperty minimumFiringRate;
    

    bool showAIControls = false;
    bool showBurstFireControls = false;

    private void OnEnable()
    {
        shooterScript = (Shooter)target;

        showAIControls = shooterScript.GetIsAI();

        firingRateVariance = serializedObject.FindProperty("firingRateVariance");
        minimumFiringRate = serializedObject.FindProperty("minimumFiringRate");
        projectilePrefab = serializedObject.FindProperty("projectilePrefab");
        firingType = serializedObject.FindProperty("firingType");
        projectileSpeed = serializedObject.FindProperty("projectileSpeed");
        projectileLifetime = serializedObject.FindProperty("projectileLifetime");
        baseFiringRate = serializedObject.FindProperty("baseFiringRate");
        projectilesPerBurst = serializedObject.FindProperty("projectilesPerBurst");
        timeBetweenBurstShots = serializedObject.FindProperty("timeBetweenBurstShots");
        coneSpread = serializedObject.FindProperty("coneSpread");
        projectileRotateSpeed = serializedObject.FindProperty("projectileRotateSpeed");
        
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GeneralSetting();
        AIControls();
        FiringTypeSettings();
        

        serializedObject.ApplyModifiedProperties();   
    }

    private void GeneralSetting()
    {
        EditorGUILayout.LabelField("General Settngs", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(projectilePrefab);
        EditorGUILayout.PropertyField(firingType);
        EditorGUILayout.PropertyField(projectileSpeed);
        EditorGUILayout.PropertyField(projectileLifetime);
        EditorGUILayout.PropertyField(baseFiringRate);
    }

    void AIControls()
    {
        EditorGUILayout.LabelField("AI Settings", EditorStyles.boldLabel);

        shooterScript.isAI = GUILayout.Toggle(shooterScript.isAI, "Is AI");

        if (shooterScript.isAI)
        {
            showAIControls = EditorGUILayout.Foldout(showAIControls, "AI Controls", true);
            if (showAIControls)
            {
                EditorGUILayout.PropertyField(firingRateVariance);
                EditorGUILayout.PropertyField(minimumFiringRate);
            }
        }
    }

    void FiringTypeSettings()
    {
        if (shooterScript.GetFiringType() == FiringTypes.Burst)
        {
            BurstSettings();
        }
        else if(shooterScript.GetFiringType() == FiringTypes.Cone)
        {
            ConeSettings();
        }
        else { return; }
    }

    void BurstSettings()
    {
        EditorGUILayout.LabelField("Burst Fire Settings", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(projectilesPerBurst);
        EditorGUILayout.PropertyField(timeBetweenBurstShots);
    }

    void ConeSettings()
    {
        EditorGUILayout.LabelField("Burst Fire Settings", EditorStyles.boldLabel);

        EditorGUILayout.PropertyField(coneSpread);
        EditorGUILayout.PropertyField(projectileRotateSpeed);
    }

}
