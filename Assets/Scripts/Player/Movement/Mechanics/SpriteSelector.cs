using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSelector : MonoBehaviour
{
    [Header("Jump Settings:"), Min(0)]
    [SerializeField] private float squashTime = .05f;
    [Min(0)]
    [SerializeField] private float midTime = .05f;
    [Min(0)]
    [SerializeField] private float stretchTime = .05f;

    public Sprite left, right, leftSquash, rightSquash, leftStretch, rightStretch, leftHurt, rightHurt;

    public AudioClip hurtSFX;
    private AudioSource playerSFX;

    private SpriteRenderer sr;

    private bool isSquash = false, isStretch = false;
    public bool isHurt=false;
    private float direction=1;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        playerSFX = GetComponent<AudioSource>();
    }
    public void SelectSprite()
    {
        if (direction < 0)
        {
            if (isHurt)
            {
                sr.sprite = leftHurt;
            }
            else if (isSquash)
            {
                sr.sprite = leftSquash;
            }
            else if (isStretch)
            {
                sr.sprite = leftStretch;
            }
            else
            {
                sr.sprite = left;
            }
        }
        else if (direction > 0)
        {
            if (isHurt)
            {
                sr.sprite = rightHurt;
            }
            else if (isSquash)
            {
                sr.sprite = rightSquash;
            }
            else if (isStretch)
            {
                sr.sprite = rightStretch;
            }
            
            else
            {
                sr.sprite = right;
            }
        }
    }
    public void SetDirection(float dir)
    {
        if (dir!=0)
        {
            direction = dir;
            SelectSprite();
        }
    }
    public void JumpSequence()
    {
        StartCoroutine(JumpCourountine());
    }
    public void LandSequence()
    {
        StartCoroutine(LandCourountine());
    }
    public void HurtSequence()
    {
        isHurt = true;
        playerSFX.PlayOneShot(hurtSFX);
        Invoke(nameof(NotHurt), .16f);
        SelectSprite();
    }
    private void Normal()
    {
        isSquash = false;
        isStretch = false;
        SelectSprite();
    }
    private void Squash()
    {
        isSquash = true;
        isStretch = false;
        SelectSprite();
    }
    private void Stretch()
    {
        isSquash = false;
        isStretch = true;
        SelectSprite();
    }
    private void NotHurt()
    {
        isHurt = false;
    }

    IEnumerator JumpCourountine()
    {
        Squash();
        yield return new WaitForSeconds(squashTime);
        Normal();
        yield return new WaitForSeconds(midTime);
        Stretch();
        yield return new WaitForSeconds(stretchTime);
        Normal();

    }
    IEnumerator LandCourountine()
    {
        Stretch();
        yield return new WaitForSeconds(stretchTime / 2f);
        Normal();
        yield return new WaitForSeconds(midTime / 2f);
        Squash();
        yield return new WaitForSeconds(squashTime / 2f);
        Normal();
    }
}
