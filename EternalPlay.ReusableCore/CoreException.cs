#region License
/*	
Microsoft Reciprocal License (Ms-RL)

This license governs use of the accompanying software. If you use the software, you accept this license. If you do not accept the license, do not use the software.

1. Definitions
The terms "reproduce," "reproduction," "derivative works," and "distribution" have the same meaning here as under U.S. copyright law.
A "contribution" is the original software, or any additions or changes to the software.
A "contributor" is any person that distributes its contribution under this license.
"Licensed patents" are a contributor's patent claims that read directly on its contribution.

2. Grant of Rights
(A) Copyright Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free copyright license to reproduce its contribution, prepare derivative works of its contribution, and distribute its contribution or any derivative works that you create.
(B) Patent Grant- Subject to the terms of this license, including the license conditions and limitations in section 3, each contributor grants you a non-exclusive, worldwide, royalty-free license under its licensed patents to make, have made, use, sell, offer for sale, import, and/or otherwise dispose of its contribution in the software or derivative works of the contribution in the software.

3. Conditions and Limitations
(A) Reciprocal Grants- For any file you distribute that contains code from the software (in source code or binary format), you must provide recipients the source code to that file along with a copy of this license, which license will govern that file. You may license other files that are entirely your own work and do not contain code from the software under any terms you choose.
(B) No Trademark License- This license does not grant you rights to use any contributors' name, logo, or trademarks.
(C) If you bring a patent claim against any contributor over patents that you claim are infringed by the software, your patent license from such contributor to the software ends automatically.
(D) If you distribute any portion of the software, you must retain all copyright, patent, trademark, and attribution notices that are present in the software.
(E) If you distribute any portion of the software in source code form, you may do so only under this license by including a complete copy of this license with your distribution. If you distribute any portion of the software in compiled or object code form, you may only do so under a license that complies with this license.
(F) The software is licensed "as-is." You bear the risk of using it. The contributors give no express warranties, guarantees or conditions. You may have additional consumer rights under your local laws which this license cannot change. To the extent permitted under your local laws, the contributors exclude the implied warranties of merchantability, fitness for a particular purpose and non-infringement.
*/
#endregion

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace EternalPlay.ReusableCore {
    /// <summary>
    /// Foundational exception type.
    /// </summary>
    /// <remarks>
    /// Any more customized exceptions should derive from this class.
    /// </remarks>
    [Serializable]
    public class CoreException : Exception {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoreException">LoggingException</see> class.
        /// </summary>
        /// <remarks>
        /// <para>
        /// This constructor initializes the <see cref="Exception.Message">Message</see> property of the new
        /// instance to a system-supplied message that describes the error and takes into account the current system
        /// culture.  This constructor will leave the <see cref="Exception.InnerException">InnerException</see>
        /// property null.
        /// </para>
        /// <para>
        /// All derived classes should provide this default constructor.
        /// </para>
        /// </remarks>
        public CoreException()
            : base() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreException">LoggingException</see> class with a specified
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error. 
        /// </param>
        public CoreException(string message)
            : base(message) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreException">LoggingException</see> class with a specified
        /// error message and a reference to the inner exception that is the cause of this exception.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error. 
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if
        /// no inner exception is specified.
        /// </param>
        public CoreException(string message, Exception innerException)
            : base(message, innerException) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoreException">LoggingException</see> class with serialized
        /// data. 
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo">SerializationInfo</see> that holds the serialized object data about the
        /// exception being thrown. 
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext">StreamingContext</see> that contains contextual information about the
        /// source or destination.
        /// </param>
        protected CoreException(SerializationInfo info, StreamingContext context)
            : base(info, context) {

            //FUTUREDEV:  Deserialize any custom class data from the serialization stream

            //if (info != null) {
            //    this.Property = info.GetString("Property");
            //}
        }

        /// <summary>
        /// Gets object data for serializing the exception
        /// </summary>
        /// <param name="info">
        /// The <see cref="SerializationInfo">SerializationInfo</see> that holds the serialized object data about the
        /// exception being thrown. 
        /// </param>
        /// <param name="context">
        /// The <see cref="StreamingContext">StreamingContext</see> that contains contextual information about the
        /// source or destination.
        /// </param>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context) {
            //NOTE:  Let the base exception objects serialize their data
            base.GetObjectData(info, context);

            //FUTUREDEV: Add class custom data to the serialization stream
            //if (info != null) {
            //    info.AddValue("Property", this.Property);
            //}
        }
    }
}
