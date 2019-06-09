using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMATHLIB 
{


    //-----------------------------------DFT------------------------------------//
    //
    //Discrete Fourier Transformation
    //
    //
    // This is the discrete fourier transformation. It takes a vector with float values and
    // gives back a vector where the values are complex numbers represented by a float array of lenght 2


    public static float[,] FourierTransform(float[] InputSignal)
    {
        int SampleCount = InputSignal.Length;
        float SampleCountFloat = InputSignal.Length;

        //This is used as return array. Note that FArray[a,0] is the real part for various 0<=a<=InputSignal.Length
        // and  FArray[a,1] is the imaginary part for various 0<=a<=InputSignal.Length
        float[,] FArray = new float[SampleCount, 2];

        for (int i = 0; i < SampleCount; i++)
        {

            FArray[i, 0] = 0;
            FArray[i, 1] = 0;
            for (int j = 0; j < SampleCount; j++)
            {
                FArray[i, 0] = FArray[i, 0] + InputSignal[j] * Mathf.Cos(2 * Mathf.PI * i * j / SampleCountFloat);
                FArray[i, 1] = FArray[i, 1] + InputSignal[j] * Mathf.Sin(-2 * Mathf.PI * i * j / SampleCountFloat);
            }

        }


        return FArray;


    }


    //Inverse Discrete Fourier transformation (IDFT)
    //
    //
    // Note that the IDFT is a transformation of a vectors where the values are complex numbers (see wikipedia)
    // Here we represent a complex number as a array of float numbers [a,b]=a+bi
    // Note that we have implemented a complex multiplication where it takes two float arrays of lenght 2 and gives back
    // a float array of lenght two which represends the multiplication.

    public static float[,] IFourierTransformation(float[,] InputSignal)
    {
        int SampleCount = InputSignal.GetLength(0);
        float SampleCountFloat = InputSignal.Length;

        // The return variable. The FArray[-,0] represents the real part and
        // FArray[-,1] represents the imaginary part
        float[,] FArray = new float[SampleCount, 2];

        // Temporary arrays for  calcualations involved IFOurierTransformation. 
        // They have lenght 2 because they  represent a complex number hence
        // two dimensional float.
        float[] ComplexTemp = new float[2];
        float[] ComplexTemp2 = new float[2];
        float[] ComplexTempM = new float[2];

        for (int i = 0; i < SampleCount; i++)
        {
            FArray[i, 0] = 0;
            FArray[i, 1] = 0;
            for (int j = 0; j < SampleCount; j++)
            {
                ComplexTemp[0] = Mathf.Cos(4 * Mathf.PI * i * j / SampleCountFloat);
                ComplexTemp[1] = Mathf.Sin(4 * Mathf.PI * i * j / SampleCountFloat);
                ComplexTemp2[0] = InputSignal[j, 0];
                ComplexTemp2[1] = InputSignal[j, 1];
                ComplexTempM = ComplexMultiplication(ComplexTemp, ComplexTemp2);
                FArray[i, 0] = FArray[i, 0] + ComplexTempM[0];
                FArray[i, 1] = FArray[i, 1] + ComplexTempM[1];

            }

            FArray[i, 0] = FArray[i, 0] / SampleCountFloat;
            FArray[i, 1] = FArray[i, 1] / SampleCountFloat;
        }


        return FArray;

    }



    //Complex multiplication
    public static float[] ComplexMultiplication(float[] A, float[] B)
    {
        //setting the return
        float[] Prod = new float[2];

        //Setting the real part of the product
        Prod[0] = A[0] * B[0] - A[1] * B[1];
        //Setting the imaginary part of the product
        Prod[1] = A[0] * B[1] + A[1] * B[0];

        return Prod;
    }




     //--------------------Signal-Processing----------------------//




    //Extracts the volume values of an AudioClip in a float Array
    //
    public static float[] ExtractLevelValues(AudioClip InputAudio)
    {
        float[] OutputArray = new float[InputAudio.samples];
        // here the OffsetSamples parameter is zero. OffsetSamples parameter  starts reading from a  specific position of the clip.
        InputAudio.GetData(OutputArray, 0);
        return OutputArray;

    }


}
